package com.eliasb.apz_android.model

data class UpdateAccountRequest(
    val login: String,
    val email: String,
    val firstName: String,
    val lastName: String,
    val phone: String
)