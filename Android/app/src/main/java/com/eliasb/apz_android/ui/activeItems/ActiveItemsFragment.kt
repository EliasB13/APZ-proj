package com.eliasb.apz_android.ui.activeItems

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import com.eliasb.apz_android.R
import com.eliasb.apz_android.adapters.ItemsAdapter
import kotlinx.android.synthetic.main.fragment_items_list.*

class ActiveItemsFragment : Fragment() {

    private lateinit var adapter: ItemsAdapter

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        return inflater.inflate(R.layout.fragment_items_list, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        adapter = ItemsAdapter(context!!)
        adapter.refreshActiveItems(empty_list, activeItemsList)

        activeItemsList.layoutManager = LinearLayoutManager(context)
        activeItemsList.adapter = adapter
    }
}
