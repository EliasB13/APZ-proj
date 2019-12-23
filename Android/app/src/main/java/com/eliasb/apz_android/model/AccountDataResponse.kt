package com.eliasb.apz_android.model

data class AccountDataResponse(
    val id: Int,
    val login: String,
    val email: String,
    val firstName: String,
    val lastName: String,
    val phone: String,
    val photo: String
)