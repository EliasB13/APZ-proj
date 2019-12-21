(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d2086b7"],{a55b:function(t,e,s){"use strict";s.r(e);var n=function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",{staticClass:"row justify-content-center"},[s("div",{staticClass:"col-lg-5 col-md-7"},[s("div",{staticClass:"card bg-secondary shadow border-0"},[s("div",{staticClass:"card-header bg-transparent pb-5"},[s("div",{staticClass:"text-muted text-center mt-2 mb-3"},[s("small",[t._v(t._s(t.$t("loginPage.signInAsHeader")))])]),s("div",{staticClass:"btn-wrapper text-center"},[s("base-button",{attrs:{type:t.privateUserSelected?"primary":"secondary",icon:"ni ni-single-02"},on:{click:t.privateUserClick}},[t._v(t._s(t.$t("loginPage.privateUser")))]),s("base-button",{attrs:{type:t.businessUserSelected?"primary":"secondary",icon:"ni ni-building"},on:{click:t.businessUserClick}},[t._v(t._s(t.$t("loginPage.businessUser")))])],1)]),s("div",{staticClass:"card-body px-lg-5 py-lg-5"},[s("form",{attrs:{role:"form"}},[s("base-input",{staticClass:"input-group-alternative mb-3",attrs:{placeholder:t.$t("loginPage.placeholder.login"),"addon-left-icon":"ni ni-email-83"},model:{value:t.loginInput,callback:function(e){t.loginInput=e},expression:"loginInput"}}),s("base-input",{staticClass:"input-group-alternative",attrs:{placeholder:t.$t("loginPage.placeholder.password"),type:"password","addon-left-icon":"ni ni-lock-circle-open"},model:{value:t.passwordInput,callback:function(e){t.passwordInput=e},expression:"passwordInput"}}),s("base-checkbox",{staticClass:"custom-control-alternative"},[s("span",{staticClass:"text-muted"},[t._v(t._s(t.$t("loginPage.button.rememberMe")))])]),s("div",{staticClass:"text-center"},[t.showSpinner?s("b-spinner",{staticClass:"mr-3"}):t._e(),s("base-button",{staticClass:"my-4",attrs:{type:"primary"},on:{click:t.handleSignIn}},[t._v(t._s(t.$t("loginPage.button.signIn")))])],1)],1)])]),s("div",{staticClass:"row mt-3"},[s("div",{staticClass:"col-6"},[s("a",{staticClass:"text-light",attrs:{href:"#"}},[s("small",[t._v(t._s(t.$t("loginPage.button.forgotPassword")))])])]),s("div",{staticClass:"col-6 text-right"},[s("router-link",{staticClass:"text-light",attrs:{to:"/register"}},[s("small",[t._v(t._s(t.$t("loginPage.button.signUpRedirect")))])])],1)])])])},i=[],r=(s("a4d3"),s("4de4"),s("4160"),s("e439"),s("dbb4"),s("b64b"),s("159b"),s("ade3")),a=s("2f62");function o(t,e){var s=Object.keys(t);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(t);e&&(n=n.filter((function(e){return Object.getOwnPropertyDescriptor(t,e).enumerable}))),s.push.apply(s,n)}return s}function l(t){for(var e=1;e<arguments.length;e++){var s=null!=arguments[e]?arguments[e]:{};e%2?o(Object(s),!0).forEach((function(e){Object(r["a"])(t,e,s[e])})):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(s)):o(Object(s)).forEach((function(e){Object.defineProperty(t,e,Object.getOwnPropertyDescriptor(s,e))}))}return t}var c={name:"loginView",data:function(){return{loginInput:"",passwordInput:"",privateUserSelected:!0,businessUserSelected:!1,isSubmitted:!1}},computed:l({},Object(a["c"])({account:function(t){return t.account.status},alert:function(t){return t.alert}}),{isLoginValid:function(){return""!==this.loginInput||this.isSubmitted?this.loginInput.length>4&&this.loginInput.length<24:null},getLoginError:function(){return""===this.loginInput?"":this.loginInput.length<5||this.loginInput.length>23?"Login must be more than 4 letters and less than 24":this.loginInput.charAt(0)>="0"&&this.loginInput.charAt(0)<="9"?"Login can't starts with digit":/^[a-zA-z0-9]+$/.test(this.loginInput)?"":"Login can contain only latin chars"},isPasswordValid:function(){return""!==this.passwordInput||this.isSubmitted?!(this.passwordInput.length<6||this.passwordInput.length>23):null},getPasswordError:function(){return(""!==this.passwordInput||this.isSubmitted)&&(this.passwordInput.length<6||this.passwordInput.length>23)?"Password must be more than 5 symbols and less than 24":""},showSpinner:function(){return this.account.loggingIn}}),created:function(){this.logout()},methods:l({},Object(a["b"])("account",["login","logout"]),{},Object(a["b"])("alert",["clear"]),{handleSignIn:function(){this.isSubmitted=!0;var t=this.loginInput,e=this.passwordInput;t&&e&&this.login({login:t,password:e,isBusinessUser:this.businessUserSelected})},clearStoreAlerts:function(){this.clear()},businessUserClick:function(){this.privateUserSelected&&(this.privateUserSelected=!1,this.businessUserSelected=!0)},privateUserClick:function(){this.businessUserSelected&&(this.businessUserSelected=!1,this.privateUserSelected=!0)}})},u=c,p=s("2877"),d=Object(p["a"])(u,n,i,!1,null,null,null);e["default"]=d.exports}}]);
//# sourceMappingURL=chunk-2d2086b7.b7e23177.js.map