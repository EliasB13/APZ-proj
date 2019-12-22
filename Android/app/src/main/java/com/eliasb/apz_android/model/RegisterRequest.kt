package com.eliasb.apz_android.model

data class RegisterRequest(
    val login: String,
    val firstName: String,
    val lastName: String,
    val email: String,
    val password: String,
    val passwordConfirmation: String
)