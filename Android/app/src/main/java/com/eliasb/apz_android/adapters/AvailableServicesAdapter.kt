package com.eliasb.apz_android.adapters

import android.content.Context
import android.util.Log
import androidx.recyclerview.widget.RecyclerView
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.Toast
import com.eliasb.apz_android.R
import com.eliasb.apz_android.model.ServiceResponse
import com.eliasb.apz_android.services.AvailableServicesService
import com.eliasb.apz_android.services.PreferencesService
import com.squareup.picasso.Picasso


import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.fragment_available_services_item.view.*

class AvailableServicesAdapter(val context: Context
) : RecyclerView.Adapter<AvailableServicesAdapter.ServicesViewHolder>() {

    val client by lazy { AvailableServicesService.create() }
    val services: ArrayList<ServiceResponse> = ArrayList()

    init {
        refreshServices()
    }

    class ServicesViewHolder(val view: View) : RecyclerView.ViewHolder(view) {
        private val serviceImage: ImageView = view.findViewById(R.id.photo)

        fun updateWithUrl(url: String) {
            Picasso.get().load(url).into(serviceImage)
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ServicesViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.fragment_available_services_item, parent, false)

        return ServicesViewHolder(view)
    }

    override fun onBindViewHolder(holder: ServicesViewHolder, position: Int) {
        holder.view.companyName.text = services[position].companyName
        holder.view.address.text = services[position].address
        holder.view.description.text = services[position].description
        val imageUrl = "https://apz-backend.azurewebsites.net/" + if (services[position].photo == null) "resources/profilepics/default_photo.png" else services[position].photo
        holder.updateWithUrl(imageUrl)
        Log.i("ImageUrl", imageUrl)
        holder.view.setOnClickListener{ Log.i("INFO", "click!!!") }
    }

    override fun getItemCount(): Int = services.size

    fun refreshServices() {
        val prefService = PreferencesService
        prefService.create(context, context.getString(R.string.token_pref))
        val token = prefService.getPreference(context.getString(R.string.token))
        client.getAvailableServices("Bearer $token")
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({
                result -> Log.i("Res", result.toString())
                services.clear()
                services.addAll(result)
                notifyDataSetChanged()
            },
            {
                error ->
                Toast.makeText(context, "Refresh error: ${error.message}", Toast.LENGTH_LONG).show()
                Log.e("ERRORS", error.message)
            })
    }
}
