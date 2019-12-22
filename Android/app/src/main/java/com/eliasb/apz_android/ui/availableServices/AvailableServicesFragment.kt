package com.eliasb.apz_android.ui.availableServices

import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.eliasb.apz_android.R
import com.eliasb.apz_android.adapters.AvailableServicesAdapter

import kotlinx.android.synthetic.main.fragment_available_services.*

class AvailableServicesFragment : Fragment() {

    lateinit var adapter: AvailableServicesAdapter

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_available_services, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        adapter = AvailableServicesAdapter(context!!)

        list.layoutManager = LinearLayoutManager(context)
        list.adapter = adapter
    }
}
