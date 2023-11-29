import $ from "jquery";
import Swal from "sweetalert2";

import pages from "./pages/index";

window.qpl = {
    pages: pages
};

$(() => {
    $("#menu-toggle").on("click", function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });

    const showSidebar = (toggleId, sidebarId, mainId) =>{
        const toggle = document.getElementById(toggleId),
        sidebar = document.getElementById(sidebarId),
        main = document.getElementById(mainId)
     
        if(toggle && sidebar && main){
            toggle.addEventListener('click', ()=>{
                /* Show sidebar */
                sidebar.classList.toggle('show-sidebar')
                /* Add padding main */
                main.classList.toggle('main-pd')
            })
        }
     }
     showSidebar('header-toggle','sidebar', 'main')
     
});
 
export default 1;