import Vue from "vue";
import VueRouter from "vue-router";

const HomePage = () => import("../views/home");
const RegisterPage = () => import("../views/register");
const LoginPage = () => import("../views/login");
const MiscPage = () => import("../views/misc");

Vue.use(VueRouter);

const routes = [
    {
		path: "/",
		components: {
			default: HomePage,
		},
		meta: {
			title: "Home",
		},
	},
	{
		name: "login",
		path: "/login",
		component: LoginPage,
		meta: {
			title: "Login",
		},
	},
	{
		name: "register",
		path: "/register",
		component: RegisterPage,
		meta: {
			title: "register",
		},
	},
	{
		name: "misc",
		path: "/misc",
		component: MiscPage,
		meta: {
			title: "misc",
		},
	},
]

//Config
const router = new VueRouter({
	routes,
	base: "/",
	mode: "history",
});

router.beforeEach((to, from, next) => {
	// redirect to login page if not logged in and trying to access a restricted page
	if (to.fullPath.substr(0, 2) === "/#") {
		const path = to.fullPath.substr(2);
		next(path);
		return;
	}
	const notAllowedIfLoginPages = ["login", "register"];
	const publicPages = [
		"404",
		...notAllowedIfLoginPages,
	];
	const authRequired = !publicPages.includes(to.name);
	const loggedIn = localStorage.getItem("userToken");
	if (authRequired && !loggedIn) {
		return next("/login?callbackUrl=" + to.path);
	}
	if (notAllowedIfLoginPages.includes(to.name) && loggedIn) return next("/");
	next();
});

export default router;