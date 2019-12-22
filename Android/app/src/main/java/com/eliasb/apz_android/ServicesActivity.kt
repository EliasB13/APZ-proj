package com.eliasb.apz_android

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.eliasb.apz_android.R

class ServicesActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_services)
        supportActionBar?.hide()
    }
}
