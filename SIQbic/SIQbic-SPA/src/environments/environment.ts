// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  securitasApiUrl: "http://localhost:5000/Auth/",
  photosAPIUrl: "http://majahide-001-site1.itempurl.com/releasecandidates/PhotosManagerAPI/", //"http://localhost:5100/"
  emailsApiUrl: "http://localhost:5001/", //"http://majahide-001-site1.itempurl.com/releasecandidates/EmailsManagerAPI/"
  emailSupportApiKey: "a0ba6866-4e22-47b5-8891-b13f9811c3c0"
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
