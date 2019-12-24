package com.eliasb.apz_android.ui.profile

import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.*
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.Navigation
import com.eliasb.apz_android.MainActivity
import com.eliasb.apz_android.R
import com.eliasb.apz_android.config.ApiConfig
import com.eliasb.apz_android.model.AccountDataResponse
import com.eliasb.apz_android.services.AccountService
import com.eliasb.apz_android.services.PreferencesService
import com.makeramen.roundedimageview.RoundedTransformationBuilder
import com.squareup.picasso.Picasso
import com.squareup.picasso.Transformation
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.fragment_profile.*


class ProfileFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_profile, container, false)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setHasOptionsMenu(true)
    }

    override fun onCreateOptionsMenu(menu: Menu?, inflater: MenuInflater?) {
        super.onCreateOptionsMenu(menu, inflater)
        menu?.add(0, 0, 0, "Logout")?.setIcon(R.drawable.ic_logout)?.setShowAsAction(MenuItem.SHOW_AS_ACTION_ALWAYS)
    }

    override fun onOptionsItemSelected(item: MenuItem?): Boolean {
        if (item?.title == "Logout")
            AccountService.logout(context!!)
        return super.onOptionsItemSelected(item)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        loadProfile()
    }

    private fun loadProfile() {
        val prefService = PreferencesService
        prefService.create(context!!, context!!.getString(R.string.user_pref))
        val token = prefService.getPreference(context!!.getString(R.string.token))
        if (token != null) {
            val client = AccountService.create()
            client.getProfile("Bearer $token")
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                    {
                        result ->
                        Log.i("GetAccData res", result.toString())
                        result.body()?.let {
                            updateUi(it)
                        }
                        result.errorBody()?.let {
                            Toast.makeText(context, getString(R.string.fetch_err), Toast.LENGTH_SHORT).show()
                        }
                    },
                    {
                        error ->
                        Toast.makeText(context, "Fetching account data error: ${error.message}", Toast.LENGTH_LONG).show()
                        Log.e("GetAccData err", error.message)
                    }
                )
        }
    }

    private fun updateUi(response: AccountDataResponse) {
        val url = ApiConfig.getBaseUrl() + "/" + response.photo
        val transformation = getRoundImageTransformation()
        Picasso.get().load(url).fit().transform(transformation).into(photo)

        login_top.text = response.login
        login.text = response.login
        email.text = response.email
        firstName.text = response.firstName
        lastName.text = response.lastName
        phone.text = response.phone


        val bundle = Bundle()
        bundle.putSerializable(context!!.getString(R.string.account_data), response)

        editFab.setOnClickListener(Navigation.createNavigateOnClickListener(R.id.action_nav_profile_to_editProfileFragment, bundle))
    }

    private fun getRoundImageTransformation(): Transformation = RoundedTransformationBuilder()
            .borderColor(Color.BLACK)
            .borderWidthDp(3f)
            .cornerRadiusDp(30f)
            .oval(false)
            .build()
}
