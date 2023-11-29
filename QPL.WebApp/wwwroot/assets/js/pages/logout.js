
// var sessionTimeoutInMinutes = 0.30; // Oturum zaman aşımı süresi (20 dakika)
// var logout = false;
// var timeoutHandle;

// startSessionTimeout()

// // Sayfa yüklenirken oturum süresi sayacı başlatılır
// function startSessionTimeout() {

//     timeoutHandle = setTimeout(sessionTimeout, sessionTimeoutInMinutes * 60 * 1000);
// }

// // Kullanıcı sayfa üzerinde herhangi bir işlem yaptığında oturum süresi sayacı sıfırlanır
// // function resetSessionTimeout() {
// //     clearTimeout(timeoutHandle);
// //     startSessionTimeout();
// // }

// // Oturum süresi dolunca oturumu sonlandırma işlemi yapılabilir
// function sessionTimeout() {
//     debugger
//     deletePageSpecificData()
//     deleteAllCookies()

//     logout = true;
//     // window.location.href = "/Account/Test";
//     // Oturumu sonlandırma veya belirli bir işlem yapma kodları burada yer alabilir
//     // Örneğin, oturumu sonlandırma işlemi yapmak için uygun bir yönlendirme yapılabilir.
// }
// function deleteAllCookies() {
//     debugger
//     var cookies = document.cookie.split(";");
  
//     for (var i = 0; i < cookies.length; i++) {
//       var cookie = cookies[i];
//       var eqPos = cookie.indexOf("=");
//       var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
//       document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
//     }

//     // Local Storage
// localStorage.clear();

// // Session Storage
// sessionStorage.clear();
//   }

//   function deletePageSpecificData() {
//     // Belirli çerezleri silme
//     document.cookie = "https://localhost:7160/=; expires=Thu, 01 Jan 1970 00:00:00 GMT";

//     // Local Storage'dan veri silme
//     localStorage.removeItem("https://localhost:7160/");

//     // Session Storage'dan veri silme
//     sessionStorage.removeItem("https://localhost:7160/");
//   }