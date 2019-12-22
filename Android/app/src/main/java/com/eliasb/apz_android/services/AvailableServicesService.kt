package com.eliasb.apz_android.services

import com.eliasb.apz_android.model.ServiceResponse
import io.reactivex.Observable
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.adapter.rxjava2.RxJava2CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.http.Header

interface AvailableServicesService {
    @GET("PrivateUsers/available-services")
    fun getAvailableServices(@Header("Authorization") token: String): Observable<List<ServiceResponse>>

    @GET("BusinessUsers/public-profile")
    fun getService(): Observable<Response<ServiceResponse>>

    companion object {
        fun create(): AvailableServicesService {
            val retrofit = Retrofit.Builder()
                .addCallAdapterFactory(RxJava2CallAdapterFactory.create())
                .addConverterFactory(GsonConverterFactory.create())
                .baseUrl("https://apz-backend.azurewebsites.net")
                .build()

            return retrofit.create(AvailableServicesService::class.java)
        }
    }
}