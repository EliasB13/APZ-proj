package com.eliasb.apz_android.services

import android.content.Context
import android.content.SharedPreferences

class PreferencesService {

    companion object {
        private val prefService: PreferencesService? = null
        private var sharedPreferences: SharedPreferences? = null

        fun create(context: Context, prefName: String) {
            sharedPreferences = context.getSharedPreferences(prefName, Context.MODE_PRIVATE)

        }

        fun savePreference(key: String, value: String) {
            with (sharedPreferences?.edit()) {
                this?.putString(key, value)
                this?.commit()
            }
        }

        fun getPreference(key: String): String? {
            return sharedPreferences?.getString(key, null)
        }
    }
}