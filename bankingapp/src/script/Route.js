import AddCustomer from "@/components/AddCustomer.vue";
import DashboardPage from "@/components/DashboardPage.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    path: "/",
    name: "Dashboard",
    component: DashboardPage,
  },

  {
    path: "/add-customer",
    name: "AddCustomer",
    component: AddCustomer,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
