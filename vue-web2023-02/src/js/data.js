import MISAEnum from './base/MISAEnum'
const data = {
    /**
     * Hàm format ngày tháng về đúng dạng dd-mm-yyyy
     * Author: Vũ Quốc Anh (15/03/2023)
     */
    formatDate: function(date){
        try {
            if(date){
                date = new Date(date)
                let dateValue = date.getDate()
                if(dateValue<10){
                    dateValue = '0' + dateValue
                }
                let monthValue = date.getMonth() +1
                if(monthValue<10){
                    monthValue = '0' + monthValue
                }
                let yearValue = date.getFullYear()
                return `${dateValue}/${monthValue}/${yearValue}`
            }
            else{
                return ""
            }
        } catch (error) {
            return "";
        }
    },

    /**
     * Hàm format tiền về dạng xxx,xxx.xxx VND
     * Author: Vũ Quốc Anh (15/03/2023)
     */
    formatMoney: function(money){
        try {
            if(money){
                money = Number(money)
                money = money.toLocaleString('it-IT', {style : 'currency', currency : 'VND'});
                return money;
            }
            return "";
        } catch (error) {
            return "";
        }
    },

    formatGender: function(gender){
        try {
            if(gender == MISAEnum.Gender.Female){
                return "Nữ"
            }
            if(gender == MISAEnum.Gender.Male){
                return "Nam"
            }
            if(gender == MISAEnum.Gender.Other){
                return "Không xác định"
            }
        } catch (error) {
            console.log(error);
        }
    }
    
}

export default data;