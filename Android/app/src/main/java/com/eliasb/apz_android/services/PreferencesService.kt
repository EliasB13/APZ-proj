package com.eliasb.apz_android.services

import android.content.Context
import android.content.SharedPreferences

class PreferencesService {

    companion object {
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

        fun savePreference(key: String, value: Int) {
            with (sharedPreferences?.edit()) {
                this?.putInt(key, value)
                this?.commit()
            }
        }

        fun getPreference(key: String): String? {
            return sharedPreferences?.getString(key, null)
        }

        fun getPreferenceInt(key: String): Int? {
            return sharedPreferences?.getInt(key, -1)
        }

        fun clearPreference(prefName: String) {
            with (sharedPreferences?.edit()) {
                this?.clear()
                this?.commit()
            }
        }
    }
}