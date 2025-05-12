import { setCookie, destroyCookie } from 'nookies';

export const saveTokens = (accessToken: string, refreshToken: string) => {
  setCookie(null, 'accessToken', accessToken, {
    maxAge: 60 * 60,
    path: '/',
    secure: true,
    sameSite: 'lax',
  });
  

  setCookie(null, 'refreshToken', refreshToken, {
    maxAge: 60 * 60 * 24 * 7, // 7 gün
    path: '/',
  });
};

export const clearTokens = () => {
  destroyCookie(null, 'accessToken');
  destroyCookie(null, 'refreshToken');
};
