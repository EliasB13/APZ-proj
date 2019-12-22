package com.eliasb.apz_android.model

import com.google.gson.annotations.SerializedName

data class LoginRequest (
    @SerializedName("login") var login: String,
    @SerializedName("password") var password: String
)