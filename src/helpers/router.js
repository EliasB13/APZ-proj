import Vue from "vue";
import Router from "vue-router";
import AuthLayout from "../layout/AuthLayout.vue";
import DashboardLayoutBusiness from "../layout/DashboardLayoutBusiness.vue";
import DashboardLayoutPrivate from "../layout/DashboardLayoutPrivate.vue";

Vue.use(Router);

export const router = new Router({
  linkExactActiveClass: "active",
  mode: "history",
  routes: [
    {
      path: "/",
      redirect: "business-items",
      component: DashboardLayoutBusiness,
      children: [
        {
          path: "/business-items",
          name: "business items",
          component: () => import("../views/businessUsers/Items.vue")
        },
        {
          path: "/consumers",
          name: "consumers",
          component: () => import("../views/businessUsers/Consumers.vue")
        },
        {
          path: "/business-profile",
          name: "business profile",
          component: () => import("../views/businessUsers/Profile.vue")
        },
        {
          path: "/roles",
          name: "roles",
          component: () => import("../views/businessUsers/Roles.vue")
        },
        {
          path: "/role/:roleId",
          name: "role",
          component: () => import("../views/businessUsers/Role.vue"),
          props: true
        }
      ]
    },
    {
      path: "/",
      redirect: "availableServices",
      component: DashboardLayoutPrivate,
      children: [
        {
          path: "/availableServices",
          name: "available services",
          component: () =>
            import("../views/privateUsers/AvailiableServices.vue")
        },
        {
          path: "/active-items",
          name: "active items",
          component: () => import("../views/privateUsers/Items.vue")
        },
        {
          path: "/profile",
          name: "profile",
          component: () => import("../views/privateUsers/Profile.vue")
        },
        {
          path: "/service/:serviceId",
          name: "service",
          component: () => import("../views/privateUsers/Service.vue"),
          props: true
        }
      ]
    },
    {
      path: "/",
      redirect: "login",
      component: AuthLayout,
      children: [
        {
          path: "/login",
          name: "login",
          component: () => import("../views/Login.vue")
        },
        {
          path: "/register",
          name: "register",
          component: () => import("../views/Register.vue")
        }
      ]
    }

    // otherwise redirect to home
    //{ path: "*", redirect: "/" }
  ]
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  // const publicPages = ["/login", "/register"];
  // const authRequired = !publicPages.includes(to.path);
  // const user = localStorage.getItem("user");

  // if (authRequired && !user) {
  //   return next("/login");
  // }

  // TODO: error page when acessing business pages from private and vice versa

  next();
});
