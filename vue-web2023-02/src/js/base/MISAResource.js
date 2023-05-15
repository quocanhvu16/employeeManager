const MISAResource = {
    "VI":{
        form:{
            titleFormAdd : 'Thêm nhân viên',
            titleFormUpdate: 'Sửa nhân viên',
            description: 'Vui lòng điền các thông tin dưới đây để hoàn thành',
        },
        TitleFormAdd: "Thêm nhân viên",
        TitleFormUpdate: "Sửa nhân viên",
        error:{
            //Hiện thông tin lỗi trên dialog khi validate
            employeeCodeEmpty:"Mã không được để trống",
            sameEmployeeCode:"Mã này đã tồn tại",
            fullNameEmpty:"Tên không được để trống",
            fullNameMalformed:"Tên không đúng định dạng",
            bitrhdayWrong:"Ngày tháng năm vượt quá hiện tại",
            departmentEmpty:"Đơn vị không được để trống",
            identityMalformed:"CMND chỉ được chứa chữ số",
            phoneNumberMalformed:"SĐT chỉ được chứa chữ số",
            salaryMalformed:"Lương chỉ được chứa chữ số",
            emailMalformed:"Email sai định dạng",
        },
        toast:{
            //Hiện thông tin trên toast
            toastAddSuccess:"Thêm nhân viên thành công",
            toastRemoveSuccess:"Xóa nhân viên thành công",
            toastEditSuccess:"Sửa nhân viên thành công",
            toastAddError:"Thêm nhân viên thất bại",
            toastRemoveError:"Xóa nhân viên thất bại",
            toastEditError:"Sửa nhân viên thất bại",
            toastError:"Có lỗi xảy ra. Vui lòng liên hệ bên MISA",
        },
        dialog:{
            headerDialogModeDataInvalid: "Dữ liệu không hợp lệ",
            headerDialogModeDataChange: "Dữ liệu thay đổi",
            headerDialogModeRemoveEmployee: "Xóa nhân viên",
            headerDialogModeRemoveEmployees: "Xóa nhiều nhân viên",
            headerDialogModeSettingsChange:"Tùy chọn thay đổi",
            headerDialogModeSettingsDefault:"Tùy chọn mặc định",
            contentDialogModeRemoveEmployee : "Bạn có thực sự muốn xóa mã nhân viên",
            contentDialogModeRemoveEmployees : "Bạn có thực sự muốn xóa những mã nhân viên đã chọn",
            contentDialogModeDataChange:"Dữ liệu đã bị thay đổi. Bạn có muốn cất không ",
            contentDialogModeSettingsChange:"Tùy chọn đã bị thay đổi. Bạn có muốn lưu không ",
            contentDialogModeSettingsDefault:"Bạn có muốn thay đổi về tùy chọn mặc định không",
        },
        EmployeeCodeEmpty:"Mã không được để trống",
        SameEmployeeCode:"Mã này đã tồn tại",
        FullNameEmpty:"Tên không được để trống",
        FullNameMalformed:"Tên không đúng định dạng",
        BitrhdayWrong:"Ngày tháng năm vượt quá hiện tại",
        DepartmentEmpty:"Đơn vị không được để trống",
        IdentityMalformed:"CMND chỉ được chứa chữ số",
        PhoneNumberMalformed:"SĐT chỉ được chứa chữ số",
        SalaryMalformed:"Lương chỉ được chứa chữ số",
        EmailMalformed:"Email sai định dạng",
        //Hiện thông tin trên toast
        ToastAddSuccess:"Thêm nhân viên thành công",
        ToastRemoveSuccess:"Xóa nhân viên thành công",
        ToastEditSuccess:"Sửa nhân viên thành công",
        ToastAddError:"Thêm nhân viên thất bại",
        ToastRemoveError:"Xóa nhân viên thất bại",
        ToastEditError:"Sửa nhân viên thất bại",
        ToastError:"Có lỗi xảy ra. Vui lòng liên hệ bên MISA",
        //Dialog
        headerDialogModeDataInvalid: "Dữ liệu không hợp lệ",
        headerDialogModeDataChange: "Dữ liệu thay đổi",
        headerDialogModeRemoveEmployee: "Xóa nhân viên",
        headerDialogModeRemoveEmployees: "Xóa nhiều nhân viên",
        headerDialogModeSettingsChange:"Tùy chọn thay đổi",
        headerDialogModeSettingsDefault:"Tùy chọn mặc định",
        contentDialogModeRemoveEmployee : "Bạn có thực sự muốn xóa mã nhân viên",
        contentDialogModeRemoveEmployees : "Bạn có thực sự muốn xóa những mã nhân viên đã chọn",
        contentDialogModeDataChange:"Dữ liệu đã bị thay đổi. Bạn có muốn cất không ",
        contentDialogModeSettingsChange:"Tùy chọn đã bị thay đổi. Bạn có muốn lưu không ",
        contentDialogModeSettingsDefault:"Bạn có muốn thay đổi về tùy chọn mặc định không",
        //Category của menu
        categoryMenu:{
            overview: 'Tổng quan',
            employee: 'Nhân viên',
            cash : 'Tiền mặt',
            deposits : 'Tiền gửi',
            buy: 'Mua hàng',
            sell : 'Bán hàng',
            invoiceManagement :'Quản lý hóa đơn',
            warehouse : 'Kho',
            fixedAssets : 'Tài sản cố định',
            price : 'Giá thành',
            budget : 'Ngân sách',
            report : 'Báo cáo',
            financialAnalysis : 'Phân tích tài chính',
            collapse : 'Thu gọn',
            enlarge : 'Phóng to'
        },
        button:{
            addEmployee: 'Thêm mới nhân viên',
            exit: 'Hủy',
            save: 'Cất',
            saveAndAdd: 'Cất và thêm',
            no: 'Không',
            yes: 'Có',
            saveSetting: 'Lưu',
            saveDefaultSetting: 'Lấy lại mặc định',
            removeFilter: 'Bỏ lọc',
            applyFilter: 'Áp dụng'
        },
        tooltip:{
            exit: 'Thoát',
            help:'Hướng dẫn',
            exitAndQuickKeys: 'Thoát (Esc)',
            save: 'Cất (Ctrl + S)',
            saveAndAdd: 'Cất và thêm (Ctrl + shift + S)',
            exportExcel: 'Xuất ra file excel',
            customizeColumn: 'Tùy chỉnh cột',
            reload: 'Load lại dữ liệu',
            filterData: 'Lọc dữ liệu',
            edit: 'Chỉnh sửa',
            remove: 'Xóa',
            identityNumber: 'Số chứng minh nhân dân',
            identityPlace: 'Nơi cấp chứng minh nhân dân',
            identityDate : 'Ngày cấp chứng minh nhân dân',
            bankAddress : 'Chi nhánh tài khoản ngân hàng'
        },
        column:{
            employeeCode:'Mã nhân viên',
            gender:'Giới tính',
            fullName:'Tên nhân viên',
            dateOfBirth:'Ngày sinh',
            identityNumber:'Số CMND',
            identityDate:'Ngày cấp CMND',
            identityPlace:'Nơi cấp CMND',
            positionId:'Chức danh',
            departmentId:'Đơn vị',
            phoneNumber:'Số điện thoại',
            salary:'Lương',
            email:'Email',
            address:'Địa chỉ',
            bankId:'Số tài khoản',
            bankName:'Tên ngân hàng',
            bankAddress:'Chi nhánh TK ngân hàng'
        },
        input:{
            placeholder:{
                search: 'Tìm theo mã, tên nhân viên',
                employeeCode:'Nhập mã',
                fullName:'Nhập họ và tên',
                identityNumber:'Nhập số CMND',
                identityPlace:'Nhập nơi cấp',
                positionId:'- Chọn Chức danh -',
                departmentId:'- Chọn đơn vị công tác -',
                removeDepartmentId : '- Hủy chọn đơn vị -',
                phoneNumber:'Nhập số điện thoại di động',
                salary:'Nhập lương cơ bản',
                email:'Nhập email',
                address:'Nhập địa chỉ thường trú',
                bankId:'Nhập số tài khoản ngân hàng',
                bankName:'Nhập tên ngân hàng',
                bankAddress:'Nhập chi nhánh ngân hàng',
                filter:'Nhập giá trị lọc'
            },
            filter:{
                employeeCode:'Lọc mã nhân viên',
                gender:'Lọc giới tính',
                fullName:'Lọc tên nhân viên',
                dateOfBirth:'Lọc ngày sinh',
                identityNumber:'Lọc số CMND',
                identityDate:'Lọc ngày cấp CMND',
                identityPlace:'Lọc nơi cấp CMND',
                positionId:'Lọc chức danh',
                departmentId:'Lọc đơn vị',
                phoneNumber:'Lọc số điện thoại',
                salary:'Lọc lương',
                email:'Lọc email',
                address:'Lọc địa chỉ',
                bankId:'Lọc số tài khoản',
                bankName:'Lọc tên ngân hàng',
                bankAddress:'Lọc chi nhánh TK ngân hàng',
                placeholder: 'Nhập giá trị lọc'
            }
        },
        setting:{
            header : 'Tùy chỉnh cột',
        },
        filter:{
            header: 'Điều kiện lọc'
        },
        pagination:{
            total: 'Tổng',
            record: 'bản ghi',
            recordOnePage:'Số bản ghi / trang'
        },
        checkbox:{
            selected:'Đã chọn',
            unSelected:'Bỏ chọn',
            removeAll : 'Xóa tất cả',
            removeSelected: 'Xóa đã chọn',
            selectedAllPage: 'Chọn tất cả các trang'
        },
        gender:{
            female: 'Nữ',
            male: 'Nam',
            other: 'Không xác định'
        },
        filterName:{
            empty:'Trống',
            notEmpty:'Không trống',
            equal:'Bằng',
            different:'Khác',
            contain:'Chứa',
            notContain:'Không chứa',
            startWith:'Bắt đầu với',
            endWith:'Kết thúc với',
            less:'Nhỏ hơn',
            lessOrEqual:'Nhỏ hơn hoặc bằng',
            bigger:'Lớn hơn',
            biggerOrEqual:'Lớn hơn hoặc bằng'
        }
    },
    "EN":{
        Err01: "Wrong",
    }
}

export default MISAResource;