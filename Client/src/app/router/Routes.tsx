import { createBrowserRouter, Navigate } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetailsPage from "../../features/activities/details/ActivityDetailsPage";
import Counter from "../../features/counter/Counter";
import TestErrors from "../../features/error/TestErrors";
import NotFound from "../../features/error/NotFound";
import ServerError from "../../features/error/ServerError";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "activities", element: <ActivityDashboard /> },
      { path: "activities/:id", element: <ActivityDetailsPage /> },
      { path: "createActivity", element: <ActivityForm key='create' /> },
      { path: "manage/:id", element: <ActivityForm /> },
      { path: "counter", element: <Counter /> },
      { path: "error", element: <TestErrors /> },
      { path: "not-found", element: <NotFound /> },
      { path: "server-error", element: <ServerError /> },
      { path: "*", element: <Navigate replace to={'/not-found'} /> }






    ],
  },
]);
