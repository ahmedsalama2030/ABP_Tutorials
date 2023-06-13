import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },

      {
        path: '/employees',
        name: '::Menu:BasicData',
        iconClass: 'fas fa-book',
        order: 101,
        layout: eLayoutType.application,

      },
      {
        path: '/departments',
        name: '::Menu:Departments',
        parentName: '::Menu:BasicData',
        layout: eLayoutType.application,
       },
      {
        path: '/employees',
        name: '::Menu:employees',
        parentName: '::Menu:BasicData',
        layout: eLayoutType.application,
        requiredPolicy: 'BookStore.Employees',

      }

    ]);
  };
}
