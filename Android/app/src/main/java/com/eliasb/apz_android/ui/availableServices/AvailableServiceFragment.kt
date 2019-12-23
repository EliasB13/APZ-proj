package com.eliasb.apz_android.ui.availableServices

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager

import com.eliasb.apz_android.R
import com.eliasb.apz_android.adapters.ItemsAdapter
import com.eliasb.apz_android.config.ApiConfig
import com.eliasb.apz_android.services.AvailableServicesService
import com.eliasb.apz_android.services.PreferencesService
import com.squareup.picasso.Picasso
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.fragment_available_service.*

private const val SERVICE_ID_ARG = "service_id"

class AvailableServiceFragment : Fragment() {

    private var serviceId: Int? = null
    private lateinit var adapter: ItemsAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            serviceId = it.getInt(SERVICE_ID_ARG)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_available_service, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        loadService(serviceId!!)

        adapter = ItemsAdapter(context!!)
        adapter.refreshServiceItems(serviceId!!, empty_list, serviceItemsList)

        serviceItemsList.layoutManager = LinearLayoutManager(context)
        serviceItemsList.adapter = adapter
    }

    private fun loadService(serviceId: Int) {
        val prefService = PreferencesService
        prefService.create(context!!, context!!.getString(R.string.user_pref))
        val token = prefService.getPreference(context!!.getString(R.string.token))
        if (token != null) {
            val client = AvailableServicesService.create()
            client.getService(serviceId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                    {
                        result ->
                        Log.i("RefreshServiceItems res", result.toString())
                        result.body()?.let {
                            companyName.text = it.companyName
                            description.text = it.description
                            val url = ApiConfig.getBaseUrl() + "/" + if (it.photo == null) "resources/profilepics/default_photo.png" else it.photo
                            Picasso.get().load(url).into(photo)
                        }
                        result.errorBody()?.let {
                            Toast.makeText(context!!, getString(R.string.fetch_err), Toast.LENGTH_SHORT).show()
                        }
                    },
                    {
                        error ->
                        Toast.makeText(context, "Fetching service info error: ${error.message}", Toast.LENGTH_LONG).show()
                        Log.e("LoadServiceInfo err", error.message)
                    })
        }
    }
}
