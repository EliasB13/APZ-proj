package com.eliasb.apz_android.model

data class ServiceResponse(
    val id: Int,
    val login: String,
    val email: String,
    val companyName: String,
    val description: String,
    val address: String,
    val phone: String,
    val photo: String
)