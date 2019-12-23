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
import com.eliasb.apz_android.model.LoginRequest
import com.eliasb.apz_android.model.LoginResponse
import com.eliasb.apz_android.services.AccountService
import com.eliasb.apz_android.services.PreferencesService
import com.google.gson.Gson
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers

class LoginActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)
        supportActionBar?.hide()

        val signUpRedirect = findViewById<TextView>(R.id.sign_up_redirect_btn)
        signUpRedirect.setOnClickListener{ goToSignUp() }

        val signInBtn = findViewById<Button>(R.id.sign_in_btn)
        signInBtn.setOnClickListener{ signInClick() }
    }

    private fun signInClick() {
        val loginEdit = findViewById<EditText>(R.id.loginEditText)
        val passEdit = findViewById<EditText>(R.id.passEditText)
        val accService = AccountService.create()
        accService.login(LoginRequest(loginEdit.text.toString(), passEdit.text.toString()))
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({
                    loginResponse ->
                Log.d("Result", loginResponse.toString())
                loginResponse.body()?.let {
                    saveUser(it)
                    goToMain()
                }
                loginResponse.errorBody()?.let {
                    val gson = Gson()
                    val json = gson.fromJson<ErrorBody>(it.charStream(), ErrorBody::class.java)
                    Log.w("ResponseError", json.message)
                    val codeStringId = resources.getIdentifier("code_" + json.code.toString(), "string", packageName)
                    Toast.makeText(this, getString(codeStringId), Toast.LENGTH_SHORT).show()
                }
            },{ error ->
                Log.d("Error", error.message)
            })
    }

    private fun goToSignUp() {
        val intent = Intent(this, RegisterActivity::class.java)
        startActivity(intent)
    }

    private fun goToMain() {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
        finish()
    }

    private fun saveUser(response: LoginResponse) {
        val prefService = PreferencesService
        prefService.create(this, getString(R.string.user_pref))
        prefService.savePreference(getString(R.string.token), response.token)
        prefService.savePreference(getString(R.string.id), response.id)
    }
}
