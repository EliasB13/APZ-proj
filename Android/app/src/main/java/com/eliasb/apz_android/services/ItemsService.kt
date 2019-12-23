package com.eliasb.apz_android.services

import com.eliasb.apz_android.model.ItemResponse
import io.reactivex.Observable
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.Query

interface ItemsService {
    @GET("api/SharedItems")
    fun getServiceItems(@Header("Authorization") token: String, @Query("businessUserId") serviceId: Int): Observable<List<ItemResponse>>

    @GET("PrivateUsers/active-items")
    fun getActiveItems(@Header("Authorization") token: String): Observable<List<ItemResponse>>

    companion object {
        fun create(): ItemsService {
            val retrofit = RetrofitBuilder.build()

            return retrofit.create(ItemsService::class.java)
        }
    }
}