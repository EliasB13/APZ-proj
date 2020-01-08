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
  const publicPages = ["/login", "/register"];
  const authRequired = !publicPages.includes(to.path);
  const userJson = localStorage.getItem("user");
  const user = JSON.parse(userJson);

  if (authRequired && !userJson) {
    return next("/login");
  }

  const businessUserPages = [
    "/business-items",
    "/consumers",
    "/business-profile",
    "/roles",
    "/role"
  ];
  const privateUserPage = [
    "/availableServices",
    "/active-items",
    "/service",
    "/profile"
  ];
  const isNextBusinessPage = businessUserPages.includes(to.path);
  const isNextPrivatePage = privateUserPage.includes(to.path);

  if (isNextBusinessPage && !user.isBusinessUser)
    return next("/availableServices");

  if (isNextPrivatePage && user.isBusinessUser) return next("/business-items");

  next();
});
