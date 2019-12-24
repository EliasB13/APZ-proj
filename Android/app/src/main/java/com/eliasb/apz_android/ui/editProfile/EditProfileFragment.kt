package com.eliasb.apz_android.ui.editProfile


import android.graphics.Color
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.navigation.NavController
import androidx.navigation.Navigation
import androidx.navigation.fragment.findNavController

import com.eliasb.apz_android.R
import com.eliasb.apz_android.config.ApiConfig
import com.eliasb.apz_android.model.AccountDataResponse
import com.eliasb.apz_android.model.ErrorBody
import com.eliasb.apz_android.model.UpdateAccountRequest
import com.eliasb.apz_android.services.AccountService
import com.eliasb.apz_android.services.PreferencesService
import com.google.gson.Gson
import com.makeramen.roundedimageview.RoundedTransformationBuilder
import com.squareup.picasso.Picasso
import com.squareup.picasso.Transformation
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.fragment_edit_profile.*
import kotlinx.android.synthetic.main.fragment_edit_profile.login_top
import kotlinx.android.synthetic.main.fragment_edit_profile.photo
import kotlinx.android.synthetic.main.fragment_profile.*

class EditProfileFragment : Fragment() {

    private var accountData: AccountDataResponse? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        arguments?.let {
            accountData = it.getSerializable(getString(R.string.account_data)) as AccountDataResponse
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_edit_profile, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        loadUi()
    }

    private fun loadUi() {
        accountData?.let {

            val url = ApiConfig.getBaseUrl() + "/" + it.photo
            val transformation = getRoundImageTransformation()
            Picasso.get().load(url).fit().transform(transformation).into(photo)

            login_top.text = it.login
            loginEdit.setText(it.login)
            emailEdit.setText(it.email)
            firstNameEdit.setText(it.firstName)
            lastNameEdit.setText(it.lastName)
            phoneEdit.setText(it.phone)
            saveFab.setOnClickListener {
                updateAccountData()
            }
        }
    }

    private fun updateAccountData() {
        val updateAccRequest = UpdateAccountRequest(
            login = loginEdit.text.toString(),
            email = emailEdit.text.toString(),
            firstName = firstNameEdit.text.toString(),
            lastName = lastNameEdit.text.toString(),
            phone = phoneEdit.text.toString()
        )

        val prefService = PreferencesService
        prefService.create(context!!, getString(R.string.user_pref))
        val token = prefService.getPreference(getString(R.string.token))
        if (token != null) {
            val accService = AccountService.create()
            accService.updateProfile("Bearer $token", updateAccRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                {
                    response ->
                    Log.i("Update acc res", response.toString())
                    response.body()?.let {
                        Toast.makeText(context, getString(R.string.update_success), Toast.LENGTH_LONG).show()
                        findNavController().navigateUp()
                    }
                    response.errorBody()?.let {
                        val gson = Gson()
                        val json = gson.fromJson<ErrorBody>(it.charStream(), ErrorBody::class.java)
                        Toast.makeText(context, "Update profile error: ${json.message}", Toast.LENGTH_LONG).show()
                        Log.e("RefreshServiceItems err", json.message)
                    }
                }, {
                    error ->
                    Log.d("Error", error.message)
                })
        }
    }

    private fun getRoundImageTransformation(): Transformation = RoundedTransformationBuilder()
        .borderColor(Color.BLACK)
        .borderWidthDp(3f)
        .cornerRadiusDp(30f)
        .oval(false)
        .build()
}
