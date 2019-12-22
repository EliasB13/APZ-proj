package com.eliasb.apz_android

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import com.eliasb.apz_android.model.ErrorBody
import com.eliasb.apz_android.model.RegisterRequest
import com.eliasb.apz_android.services.AccountService
import com.google.gson.Gson
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers

class RegisterActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)
        supportActionBar?.hide()

        val signUpRedirect = findViewById<TextView>(R.id.sign_in_redirect_btn)
        signUpRedirect.setOnClickListener{ goToSignIn() }

        val signUpBtn = findViewById<Button>(R.id.sign_up_btn)
        signUpBtn.setOnClickListener{ signUpClick() }
    }

    private fun signUpClick() {
        val request = RegisterRequest(
            findViewById<EditText>(R.id.loginEdit).text.toString(),
            findViewById<EditText>(R.id.firstNameEdit).text.toString(),
            findViewById<EditText>(R.id.lastNameEdit).text.toString(),
            findViewById<EditText>(R.id.emailEdit).text.toString(),
            findViewById<EditText>(R.id.passEdit).text.toString(),
            findViewById<EditText>(R.id.passConfirmEdit).text.toString()
        )

        val accService = AccountService.create()
        accService.register(request)
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({ response ->
                response.errorBody()?.let {
                    Log.w("ResponseError", response.body().toString())
                    val gson = Gson()
                    val json = gson.fromJson<ErrorBody>(it.charStream(), ErrorBody::class.java)
                    val codeStringId = resources.getIdentifier(
                        "code_" + json.code.toString(),
                        "string",
                        packageName)
                    Toast.makeText(
                        this,
                        if (json.code != 0) getString(codeStringId)
                        else getString(R.string.validation_error),
                        Toast.LENGTH_SHORT).show()
                }
                if (response.errorBody() == null) {
                    Toast.makeText(
                        this,
                        getString(R.string.registerSuccess),
                        Toast.LENGTH_SHORT).show()
                    goToSignIn()
                }
            }, { error -> Log.e("Error", error.message)
        })
    }

    private fun goToSignIn() {
        val intent = Intent(this, LoginActivity::class.java)
        startActivity(intent)
        finish()
    }
}