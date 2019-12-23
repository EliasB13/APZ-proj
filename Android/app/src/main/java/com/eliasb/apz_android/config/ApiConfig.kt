package com.eliasb.apz_android.config

private const val BASE_URL = "https://apz-backend.azurewebsites.net"

class ApiConfig {
    companion object {
        fun getBaseUrl(): String = BASE_URL
    }
}