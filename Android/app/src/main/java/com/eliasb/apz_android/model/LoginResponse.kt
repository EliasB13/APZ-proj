package com.eliasb.apz_android.model

import com.google.gson.annotations.SerializedName

data class LoginResponse(
    @SerializedName("id") val id: Int,
    @SerializedName("login") val login: String,
    @SerializedName("token") val token: String,
    @SerializedName("photo") val photo: String
)