import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  if (sessionStorage.getItem("jwt"))
    return true;

  window.location.href = "/login";
  return false;
};
