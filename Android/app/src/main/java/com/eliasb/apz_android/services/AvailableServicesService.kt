package com.eliasb.apz_android.services

import com.eliasb.apz_android.model.ServiceResponse
import io.reactivex.Observable
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.adapter.rxjava2.RxJava2CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.Query

interface AvailableServicesService {
    @GET("PrivateUsers/available-services")
    fun getAvailableServices(@Header("Authorization") token: String): Observable<List<ServiceResponse>>

    @GET("BusinessUsers/public-profile")
    fun getService(@Query("id") serviceId: Int): Observable<Response<ServiceResponse>>

    companion object {
        fun create(): AvailableServicesService {
            val retrofit = RetrofitBuilder.build()

            return retrofit.create(AvailableServicesService::class.java)
        }
    }
}