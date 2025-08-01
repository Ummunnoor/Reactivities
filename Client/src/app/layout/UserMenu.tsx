import * as React from "react";
import Button from "@mui/material/Button";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import { useState } from "react";
import { Avatar, Box, ListItemIcon, ListItemText } from "@mui/material";
import { useAccount } from "../../lib/hooks/useAccount";
import { Link } from "react-router";
import { Add, Logout, Person } from "@mui/icons-material";

export default function UserMenu() {
  const { currentUser, logOutUser } = useAccount();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <Button
        color="inherit"
        size="large"
        sx={{ fontSize: "1.2rem" }}
        onClick={handleClick}
      >
        <Box display="flex" alignItems="center" gap={2}>
          <Avatar />
          {currentUser?.displayName}
        </Box>
      </Button>
      <Menu
        id="basic-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        slotProps={{
          list: {
            "aria-labelledby": "basic-button",
          },
        }}
      >
        <MenuItem component={Link} to="/createActivity" onClick={handleClose}>
          <ListItemIcon>
            <Add />
          </ListItemIcon>
          <ListItemText>Create Activity</ListItemText>
        </MenuItem>

        <MenuItem component={Link} to="/profile" onClick={handleClose}>
          <ListItemIcon>
            <Person /> 
          </ListItemIcon>
          <ListItemText>My profile</ListItemText>
        </MenuItem>

        <MenuItem
          component={Link}
          to="/logout"
          onClick= { () => {
              logOutUser.mutate();
              handleClose();
          }}>
          <ListItemIcon>
            <Logout />
          </ListItemIcon>
          <ListItemText>Logout</ListItemText>
        </MenuItem>
      </Menu>
    </>
  );
}
