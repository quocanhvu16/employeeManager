/**
 * Phím tắt ctrl + 1 : mở popup form chi tiết nhân viên
 * Author: Vũ Quốc Anh (02/04/2023)
 */
const ctrl1 ={
    beforeMount :(el, binding) => {
        el._keydownCallback = event => {
            if(event.ctrlKey){
                if (event.key === '1') {
                    event.preventDefault()
                    binding.value();
                  }
            }
          };
          document.addEventListener("keydown", el._keydownCallback);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el._keydownCallback);
        delete el._keydownCallback
    },
}

/**
 * Phím tắt ctrl + S : "Cất"
 * Author: Vũ Quốc Anh (02/04/2023)
 */
// const ctrlS ={
//     beforeMount :(el, binding) => {
//         el._keydownCallback = event => {
//             if(event.ctrlKey && event.shiftkey==false && (event.key === 's'||event.key === 'S')){
//                 console.log("Ctrl +S");
//                 event.preventDefault()
//                 binding.value();
//             }
//           };
//           document.addEventListener("keydown", el._keydownCallback,true);
//     },
//     unmounted: el => {
//         document.removeEventListener("keydown", el._keydownCallback,true);
//         delete el._keydownCallback
//     },
// }

/**
 * Phím tắt ctrl + shift + S : "Cất và thêm"
 * Author: Vũ Quốc Anh (02/04/2023)
 */
// const ctrlShiftS ={
//     beforeMount :(el, binding) => {
//         el._keydownCallback1 = event => {
//             if(event.ctrlKey && event.shiftKey&&(event.key === 'S'||event.key === 's')){
//                     event.preventDefault()
//                     console.log("Ctrl + Shifft+ S");
//                     binding.value();
//             }
//           };
//           document.addEventListener("keydown", el._keydownCallback1);
//     },
//     unmounted: el => {
//         document.removeEventListener("keydown", el._keydownCallback1);
//         delete el._keydownCallback1
//     },
// }

/**
 * Click ra ngoài 1 đối tượng nào đó
 * Author: Vũ Quốc Anh (02/04/2023)
 */
const clickOutside = {
    beforeMount :(el, binding) => {
        el.clickOutsideEvent = event => {
            // here I check that click was outside the el and his children
            if (!(el == event.target || el.contains(event.target))) {
              // and if it did, call method provided in attribute value
              binding.value();
            }
          };
          document.addEventListener("click", el.clickOutsideEvent,true);
    },
    unmounted: el => {
        document.removeEventListener("click", el.clickOutsideEvent,true);
    },
}

/**
 * Phím tắt Enter
 * Author: Vũ Quốc Anh (02/04/2023)
 */
const enter ={
    beforeMount :(el, binding) => {
        el._keydownCallback = event => {
            if (event.key === 'Enter') {
              binding.value();
            }
          };
          document.addEventListener("keydown", el._keydownCallback);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el._keydownCallback);
        delete el._keydownCallback
    },
}

/**
 * Phím tắt Esc: đóng popup
 * Author: Vũ Quốc Anh (02/04/2023)
 */
const esc ={
    beforeMount :(el, binding) => {
        el._keydownCallback = event => {
            if (event.key === 'Escape') {
              binding.value();
            }
          };
          document.addEventListener("keydown", el._keydownCallback);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el._keydownCallback);
        delete el._keydownCallback
    },
}

/**
 * Phím tắt Insert
 * Author: Vũ Quốc Anh (02/04/2023)
 */
const insert ={
    beforeMount :(el, binding) => {
        el._keydownCallback = event => {
            if (event.key === 'Insert') {
              binding.value();
            }
          };
          document.addEventListener("keydown", el._keydownCallback);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el._keydownCallback);
        delete el._keydownCallback
    },
}

/**
 * Phím tắt ctrl + d : xóa nhân viên đã chọn
 * Author: Vũ Quốc Anh (02/04/2023)
 */
const ctrld ={
    beforeMount :(el, binding) => {
        el._keydownCallback = event => {
            if(event.ctrlKey){
                if (event.key === 'd'||event.key === 'D') {
                    event.preventDefault()
                    binding.value();
                  }
            }
          };
          document.addEventListener("keydown", el._keydownCallback);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el._keydownCallback);
        delete el._keydownCallback
    },
}

const actionPress = {
    beforeMount :(el, binding) => {
        el._keydownCallback = event => {
            if(event.ctrlKey && event.shiftKey == false){
                if (binding.value.isCtrl && 
                    binding.value.isShift ==false && 
                    binding.value.key ===event.keyCode) 
                {
                    event.preventDefault()
                    binding.value.fnc()
                    return
                }
            }
            if(event.ctrlKey && event.shiftKey == true){
                if (binding.value.isCtrl && 
                    binding.value.isShift && 
                    binding.value.key ===event.keyCode) 
                {
                    event.preventDefault()
                    binding.value.fnc()
                    return
                }
            }
          };
          document.addEventListener("keydown", el._keydownCallback,true);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el._keydownCallback,true);
        delete el._keydownCallback
    },
}


export {ctrl1,clickOutside,enter,esc,ctrld,insert,actionPress}