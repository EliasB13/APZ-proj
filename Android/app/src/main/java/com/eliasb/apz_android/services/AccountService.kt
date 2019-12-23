package com.eliasb.apz_android.services

import android.content.Context
import com.eliasb.apz_android.R
import com.eliasb.apz_android.RegisterActivity
import com.eliasb.apz_android.model.LoginRequest
import com.eliasb.apz_android.model.LoginResponse
import com.eliasb.apz_android.model.RegisterRequest
import io.reactivex.Observable
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.adapter.rxjava2.RxJava2CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.POST


interface AccountService {
    @POST("PrivateUsers/authenticate-private")
    fun login(@Body loginRequest: LoginRequest): Observable<Response<LoginResponse>>

    @POST("PrivateUsers/register-private")
    fun register(@Body registerRequest: RegisterRequest): Observable<Response<Void>>

    companion object {
        fun create(): AccountService {
            val retrofit = RetrofitBuilder.build()

            return retrofit.create(AccountService::class.java)
        }

        fun logout(context: Context) {
            val prefService = PreferencesService
            prefService.create(context, context.getString(R.string.token_pref))
            prefService.clearPreference(context.getString(R.string.token_pref))
        }
    }
}