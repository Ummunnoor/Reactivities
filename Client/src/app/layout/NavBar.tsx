import {
  AppBar,
  Box,
  Container,
  LinearProgress,
  MenuItem,
  Typography,
} from "@mui/material";
import Toolbar from "@mui/material/Toolbar";
import { Group } from "@mui/icons-material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/components/MenuItemLink";
import { useStore } from "../../lib/hooks/useStore";
import { Observer } from "mobx-react-lite";
import { useAccount } from "../../lib/hooks/useAccount";
import UserMenu from "./UserMenu";

export function NavBar() {
  const { uiStore } = useStore();
  const { currentUser } = useAccount();
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            "linear-gradient(135deg, #182a73 0%, #218aae 69%, #20a7ac 89%)",
          position: "relative",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box>
              <MenuItem
                component={NavLink}
                to="/"
                sx={{ display: "flex", gap: 2 }}
              >
                <Group fontSize="large" />
                <Typography variant="h4" fontWeight="bold">
                  Reactivities
                </Typography>
              </MenuItem>
            </Box>
            <Box sx={{ display: "flex" }}>
              <MenuItemLink to="/activities">Activities</MenuItemLink>
              <MenuItemLink to="/counter">Counter</MenuItemLink>
              <MenuItemLink to="/error">Errors</MenuItemLink>
            </Box>
            <Box display="flex" alignItems="center">
              {currentUser ? (
                <UserMenu />
              ) : (
                <>
                  <MenuItemLink to="/login">Login</MenuItemLink>
                  <MenuItemLink to="/register">Register</MenuItemLink>
                </>
              )}
            </Box>
          </Toolbar>
        </Container>
        <Observer>
          {() =>
            uiStore.isLoading ? (
              <LinearProgress
                color="secondary"
                sx={{
                  position: "absolute",
                  bottom: 0,
                  right: 0,
                  left: 0,
                  height: 4,
                }}
              />
            ) : null
          }
        </Observer>
      </AppBar>
    </Box>
  );
}
