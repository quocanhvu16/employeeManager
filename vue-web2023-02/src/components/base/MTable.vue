<template>
    <table class="table">
        <thead>
            <tr class="table--head">
                <td class="table__checkbox" type="checkbox" name="table__checkbox">
                    <input type="checkbox" >
                    <span :class="['checkmark']"></span>
                </td>
                <td class="table__MNV left">
                    <p>Mã nhân viên</p>
                </td>
                <td class="table__TNV left">
                    <p>Tên nhân viên</p>
                </td>
                <td class="table__GT left" ><p>Giới tính</p></td>
                <td class="table__NS center">
                    <p>Ngày sinh</p>
                </td>
                <td class="table__CMND left">
                    <p data-c-tooltip="Số chứng minh nhân dân">Số CMND</p>
                </td>
                <td class="center"><p>Ngày cấp CMND</p></td>
                <td class="left"><p>Nơi cấp CMND</p></td>
                <td class="table__CD left"><p>Chức danh</p></td>
                <td class="table__TDV left"><p>Tên đơn vị</p></td>
                <td class="left"><p>Số điện thoại</p></td>
                <td class="table__SALARY right"><p>Lương</p></td>
                <td class="left" ><p>Email</p></td>
                <td class="left" ><p>Địa chỉ</p></td>
                <td class="table__STK left"><p>Số tài khoản</p></td>
                <td class="table__TNH left"><p>Tên ngân hàng</p></td>
                <td class="table__CNTKNH left"><p>Chi nhánh TK ngân hàng</p></td>
                <td class="table__CN center"><p>Chức năng</p></td>
            </tr>
        </thead>
        <tbody>
            <MTableRow v-for="data in datas" :key="data" :data="data"/>
        </tbody>
    </table>
</template>

<script>
export default {
    name: 'MTable',
    props:['datas'],
    created() {
        console.log("Created Table");
    },
}
</script>

<style>

</style>