package com.eliasb.apz_android

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView

class RegisterActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)
        supportActionBar?.hide()

        val signUpRedirect = findViewById<TextView>(R.id.sign_in_redirect_btn)
        signUpRedirect.setOnClickListener{ goToSignIn() }
    }

    private fun goToSignIn() {
        val intent = Intent(this, LoginActivity::class.java)
        startActivity(intent)
    }
}