package com.eliasb.apz_android.adapters

import android.content.Context
import android.graphics.Color
import android.util.Log
import androidx.recyclerview.widget.RecyclerView
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.core.content.ContextCompat
import com.eliasb.apz_android.R
import com.eliasb.apz_android.config.ApiConfig
import com.eliasb.apz_android.model.ItemResponse
import com.eliasb.apz_android.services.ItemsService
import com.eliasb.apz_android.services.PreferencesService
import com.squareup.picasso.Picasso
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.fragment_items_list_item.view.*

private const val DEFAULT_ITEM_PHOTO = "resources/profilepics/default_item.png"

class ItemsAdapter(val context: Context
) : RecyclerView.Adapter<ItemsAdapter.ItemsViewHolder>() {

    val client by lazy { ItemsService.create() }
    val items: ArrayList<ItemResponse> = ArrayList()

    class ItemsViewHolder(val view: View) : RecyclerView.ViewHolder(view) {
        private val photo: ImageView = view.findViewById(R.id.photo)

        fun updateWithUrl(url: String) {
            Picasso.get().load(url).into(photo)
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ItemsViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.fragment_items_list_item, parent, false)

        return ItemsViewHolder(view)
    }

    override fun onBindViewHolder(holder: ItemsViewHolder, position: Int) {
        holder.view.name.text = items[position].name
        holder.view.description.text = items[position].description

        val imageUrl = "${ApiConfig.getBaseUrl()}/$DEFAULT_ITEM_PHOTO"
        holder.updateWithUrl(imageUrl)
        holder.view.setOnClickListener{ Log.i("INFO", "item click!!!") }

        if (items[position].isTaken)
        {
            holder.view.status.text = context.getString(R.string.status_taken)
            holder.view.status.setTextColor(ContextCompat.getColor(context, R.color.danger))
        }
        else {
            holder.view.status.text = context.getString(R.string.status_not_taken)
            holder.view.status.setTextColor(ContextCompat.getColor(context, R.color.success))
        }
    }

    override fun getItemCount(): Int = items.size

    fun refreshServiceItems(serviceId: Int, emptyListView: TextView, itemsList: RecyclerView) {
        val prefService = PreferencesService
        prefService.create(context, context.getString(R.string.token_pref))
        val token = prefService.getPreference(context.getString(R.string.token))
        if (token != null) {
            client.getServiceItems("Bearer $token", serviceId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                {
                    result ->
                    Log.i("RefreshServiceItems res", result.toString())
                    if (result.isEmpty()) {
                        emptyListView.visibility = View.VISIBLE
                        itemsList.visibility = View.GONE
                    } else {
                        emptyListView.visibility = View.GONE
                        itemsList.visibility = View.VISIBLE
                    }
                    items.clear()
                    items.addAll(result)
                    notifyDataSetChanged()
                },
                {
                    error ->
                    Toast.makeText(context, "Refresh error: ${error.message}", Toast.LENGTH_LONG).show()
                    Log.e("RefreshServiceItems err", error.message)
                })
        }
    }

    fun refreshActiveItems(emptyListView: TextView, itemsList: RecyclerView) {
        val prefService = PreferencesService
        prefService.create(context, context.getString(R.string.token_pref))
        val token = prefService.getPreference(context.getString(R.string.token))
        if (token != null) {
            client.getActiveItems("Bearer $token")
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                {
                    result -> Log.i("RefreshServiceItems res", result.toString())
                    if (result.isEmpty()) {
                        emptyListView.visibility = View.VISIBLE
                        itemsList.visibility = View.GONE
                    } else {
                        emptyListView.visibility = View.GONE
                        itemsList.visibility = View.VISIBLE
                    }
                    items.clear()
                    items.addAll(result)
                    notifyDataSetChanged()
                },
                {
                    error ->
                    Toast.makeText(context, "Refresh error: ${error.message}", Toast.LENGTH_LONG).show()
                    Log.e("RefreshServiceItems err", error.message)
                })
        }
    }
}
