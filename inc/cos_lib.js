// ----------------------------------------------------------------------------------------------------------------
// Utility functions for parsing in Cos_DateFromFormat()
// ----------------------------------------------------------------------------------------------------------------
function Cos_isInteger(val) {
    var digits = '1234567890';
    for (var i = 0; i < val.length; i++) {
        if (digits.indexOf(val.charAt(i)) == -1) { return false; }
    }
    return true;
}

function Cos_getInt(str, i, minlength, maxlength) {
    for (var x = maxlength; x >= minlength; x--) {
        var token = str.substring(i, i + x);
        if (token.length < minlength) { return null; }
        if (Cos_isInteger(token)) { return token; }
    }
    return null;
}

// -----------------------------------------------------------------------------------------------------------------
// Cos_DateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// -----------------------------------------------------------------------------------------------------------------
function Cos_DateFromFormat(val, format) {
    val = val + '';
    format = format + '';
    var i_val = 0;
    var i_format = 0;
    var c = '';
    var token = '';
    var token2 = '';
    var x, y;
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = 1;
    var hh = now.getHours();
    var mm = now.getMinutes();
    var ss = now.getSeconds();
    var ampm = '';

    while (i_format < format.length) {
        // Get next token from format string
        c = format.charAt(i_format);
        token = '';
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == 'yyyy' || token == 'yy' || token == 'y') {
            if (token == 'yyyy') { x = 4; y = 4; }
            if (token == 'yy') { x = 2; y = 2; }
            if (token == 'y') { x = 2; y = 4; }
            year = Cos_getInt(val, i_val, x, y);
            if (year == null) { return 0; }
            i_val += year.length;
            if (year.length == 2) {
                if (year > 70) { year = 1900 + (year - 0); }
                else { year = 2000 + (year - 0); }
            }
        }
        else if (token == 'MMM' || token == 'NNN') {
            month = 0;
            for (var i = 0; i < MONTH_N.length; i++) {
                var month_name = MONTH_N[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                    if (token == 'MMM' || (token == 'NNN' && i > 11)) {
                        month = i + 1;
                        if (month > 12) { month -= 12; }
                        i_val += month_name.length;
                        break;
                    }
                }
            }
            if ((month < 1) || (month > 12)) { return 0; }
        }
        else if (token == 'EE' || token == 'E') {
            for (var i = 0; i < DAY_N.length; i++) {
                var day_name = DAY_N[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == 'MM' || token == 'M') {
            month = Cos_getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12)) { return 0; }
            i_val += month.length;
        }
        else if (token == 'dd' || token == 'd') {
            date = Cos_getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31)) { return 0; }
            i_val += date.length;
        }
        else if (token == 'hh' || token == 'h') {
            hh = Cos_getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
            i_val += hh.length;
        }
        else if (token == 'HH' || token == 'H') {
            hh = Cos_getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
            i_val += hh.length;
        }
        else if (token == 'KK' || token == 'K') {
            hh = Cos_getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
            i_val += hh.length;
        }
        else if (token == 'kk' || token == 'k') {
            hh = Cos_getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
            i_val += hh.length; hh--;
        }
        else if (token == 'mm' || token == 'm') {
            mm = Cos_getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
            i_val += mm.length;
        }
        else if (token == 'ss' || token == 's') {
            ss = Cos_getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
            i_val += ss.length;
        }
        else if (token == 'a') {
            if (val.substring(i_val, i_val + 2).toLowerCase() == 'am') { ampm = 'AM'; }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == 'pm') { ampm = 'PM'; }
            else { return 0; }
            i_val += 2;
        }
        else {
            if (val.substring(i_val, i_val + token.length) != token) { return 0; }
            else { i_val += token.length; }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length) { return 0; }
    // Is date valid for month?
    if (month == 2) {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
            if (date > 29) { return 0; }
        }
        else { if (date > 28) { return 0; } }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
        if (date > 30) { return 0; }
    }
    // Correct hours value
    if (hh < 12 && ampm == 'PM') { hh = hh - 0 + 12; }
    else if (hh > 11 && ampm == 'AM') { hh -= 12; }
    var newdate = new Date(year, month - 1, date, hh, mm, ss);
    return newdate.getTime();
}

// ----------------------------------------------------------------------------------------------------
function Cos_trimAll(sString) {
    while (sString.substring(0, 1) == ' ') {
        sString = sString.substring(1, sString.length);
    }
    while (sString.substring(sString.length - 1, sString.length) == ' ') {
        sString = sString.substring(0, sString.length - 1);
    }
    return sString;
}
//======================================================================================================
function Cos_ChkisDate(val, format) {
    var tb = document.getElementById(val);
    strTxt = Cos_trimAll(tb.value);
    if (strTxt == '')
        return false;
    var date = Cos_DateFromFormat(strTxt, format);
    if (date == 0) {
        window.alert('Ngày tháng không hợp lệ! Nhập theo định dạng: ' + format);
        tb.value = "";
        tb.focus();
        return false;
    }
    return true;
}
//=================================================================================================================
function Cos_ChkisGio(val) {
    var tb = document.getElementById(val);
    strTxt = Cos_trimAll(tb.value);

    if (strTxt == '')
        return false;
    var temp = strTxt.split(":");
    var gio = temp[0];
    var phut = temp[1];


    if (parseInt(gio) > 24) {
        window.alert('Giờ không hợp lệ! Nhập theo định dạng: HH:MM');
        tb.value = "";
        tb.focus();
        return false;
    }
    if (parseInt(phut) > 59) {
        window.alert('Giờ không hợp lệ! Nhập theo định dạng: HH:MM');
        tb.value = "";
        tb.focus();
        return false;
    }

    return true;
}
//===================================================================================================================
function Cos_InputMask(e, strMask, IDE) {
    var keynum = e.charCode ? e.charCode : e.keyCode;

    var mSo = '$';
    var mChu = '#';
    var temp = '';
    var tempJ = '';
    var lenIDE;
    var lenMask;

    strText = document.getElementById(IDE).value;
    lenIDE = strText.length;
    lenMask = strMask.length;
    if (lenMask != 0) {
        if (lenIDE < lenMask) {

            for (var i = lenIDE; i < lenMask; i++) {
                temp = strMask.substring(i, i + 1);
                tempJ = '';
                if (mSo == temp) {
                    chk = 1;
                    break;
                }
                if (mChu == temp) {
                    chk = 2;
                    break;
                }
                if ((mChu != temp) && (mSo != temp)) {
                    chk = 3;
                    for (var j = i; j < lenMask; j++) {
                        if ((strMask.substring(j, j + 1) != mSo) && (strMask.substring(j, j + 1) != mChu)) {
                            tempJ = tempJ + strMask.substring(j, j + 1);
                        }
                        else {
                            if (strMask.substring(j, j + 1) == mSo) {
                                if ((keynum < 48) || (keynum > 57)) //khong la so
                                { return false; }
                            }

                            if (strMask.substring(j, j + 1) == mChu) {
                                if (!(((keynum > 64) && (keynum < 91)) || ((keynum > 96) && (keynum < 123)))) // la chu
                                { return false; }
                            }

                            break;
                        }
                    }
                    break;
                }
            } // end for	

            if (chk == 1) {
                if ((keynum < 48) || (keynum > 57)) //khong la so
                { return false; }
            }

            if (chk == 2) {
                if (!(((keynum > 64) && (keynum < 91)) || ((keynum > 96) && (keynum < 123)))) // la chu
                { return false; }
            }

            if (chk == 3) {
                document.getElementById(IDE).value = strText + tempJ;
            }

        }
        else //else (lenIDE<=lenMask)
        {
            return false;
        } //end if (lenIDE<=lenMask)

    } //lenMask<>0
}
//====================================================================================================================
function Cos_Input_Int(obj) {
    var digits = '1234567890';
    for (var i = 0; i < obj.value.length; i++) {
        if (digits.indexOf(obj.value.charAt(i)) == -1) {
            obj.value = obj.value.replace(obj.value.charAt(i), '');
        }
    }
}
//====================================================================================================================
function Cos_Input_Decimal(obj) {
    var digits = '1234567890.,';
    for (var i = 0; i < obj.value.length; i++) {
        if (digits.indexOf(obj.value.charAt(i)) == -1) {
            obj.value = obj.value.replace(obj.value.charAt(i), '');
        }
    }
}
//====================================================================================================================
function Cos_Input_Date(obj) {
    var digits = '1234567890/';
    for (var i = 0; i < obj.value.length; i++) {
        if (digits.indexOf(obj.value.charAt(i)) == -1) {
            obj.value = obj.value.replace(obj.value.charAt(i), '');
        }
    }
}
//====================================================================================================================
//Ham nay kiem tra xem ngay thang co hop le hay khong
function isValidDate(vDay, vMonth, vYear) {
    var vDateTime, cDay, cDate
    vDateTime = vMonth + "/" + vDay + "/" + vYear
    cDate = new Date(vDateTime)
    cDay = cDate.getDate()
    if (parseInt(cDay) == parseInt(vDay)) {
        return true;
    }
    else {
        return false;
    }
}

//Ham nay tim so ngay trong mot thang , tham so i la thang , y la nam
function DaysInMonth(i, y) {
    if (i == 1 || i == 3 || i == 5 || i == 7 || i == 8 || i == 10 || i == 12) {
        return (31);
    } else if (i == 4 || i == 6 || i == 9 || i == 11) {
        return (30);
    } else if (y % 4 == 0) {
        return (29);
    }
    return (28);
}

// Ham nay tham so truyen vao phai la ngay Viet Nam va chuyen doi qua ngay English
function English_Date(strVNDate) {
    var temp = strVNDate.split("/");
    var ngay = temp[0];
    var thang = temp[1];
    var nam = temp[2];
    var dateEN = thang + "/" + ngay + "/" + nam;
    return dateEN;
}

//Ham nay kiem tra xem mot chuoi co phai la so hay khong
function isNum(str) {
    var valid = "0123456789";
    var temp;
    for (var i = 0; i < str.length; i++) {
        temp = "" + str.substring(i, i + 1);
        if (valid.indexOf(temp) == "-1")
            return false;
    }
    return true;
}

//Ham nay kiem tra xem mot chuoi co phai la ngay thang hay khong
//Tham so truyen vao la kieu ngay Viet Nam
function isDate(strVNDate) {
    if (strVNDate.length > 10) {
        return false;
    }
    else {
        var temp = strVNDate.split("/");
        if (temp.length != 3) {
            return false;
        }
        else {
            var ngay = temp[0];
            var thang = temp[1];
            var nam = temp[2];
            if ((!isNum(ngay)) || (!isNum(thang)) || (!isNum(nam))) {
                return false;
            }
            else {
                if (ngay == '01') ngay = 1
                else if (ngay == '02') ngay = 2
                else if (ngay == '03') ngay = 3
                else if (ngay == '04') ngay = 4
                else if (ngay == '05') ngay = 5
                else if (ngay == '06') ngay = 6
                else if (ngay == '07') ngay = 7
                else if (ngay == '08') ngay = 8
                else if (ngay == '09') ngay = 9
                else ngay = parseInt(temp[0]);

                if (thang == '01') thang = 1
                else if (thang == '02') thang = 2
                else if (thang == '03') thang = 3
                else if (thang == '04') thang = 4
                else if (thang == '05') thang = 5
                else if (thang == '06') thang = 6
                else if (thang == '07') thang = 7
                else if (thang == '08') thang = 8
                else if (thang == '09') thang = 9
                else thang = parseInt(temp[1]);

                nam = parseInt(temp[2]);
                if ((ngay <= 0) || (ngay >= 32) || (thang <= 0) || (thang >= 13))
                    return false;
                else {
                    if (nam.toString().length != 4) {
                        window.alert('Vui lòng nhập năm với 4 kí tự\nvà không có số 0 đầu tiên');
                        return false;
                    }
                    else {
                        if (!isValidDate(ngay, thang, nam)) return false;
                        else return true;
                    }
                }
            }
        }
    }
}

function Check_Date_VN(TextBox) {
    if (TextBox.value != '') {
        if (!isDate(TextBox.value)) {
            window.alert('Ngày tháng không hợp lệ.\nNhập theo đúng định dạng dd/mm/yyyy');
            TextBox.value = '';
            TextBox.select();
            return false;
        }
    }
}

//Ham tru hai ngay (dung de so sanh 2 ngay)
//Hai tham so ngay truyen vao la ngay VietNam
function TruNgay(strNgayBD, strNgayKT) {
    var vNgayBD = new Date(English_Date(strNgayBD));
    var vNgayKT = new Date(English_Date(strNgayKT));
    return (vNgayKT - vNgayBD);
}
function KiemTraTuNgayDenNgay(NgayBD_ID, NgayKT_ID) {
    //var r = true;
    var TuNgay = document.getElementById(NgayBD_ID);
    var DenNgay = document.getElementById(NgayKT_ID);
    if (TuNgay.value == '' && DenNgay.value != '') {
        TuNgay.value = '01/01/1901';
        //window.alert('Vui lòng nhập lại "Từ ngày"');
        //TuNgay.select();
        //r = false;
    }
    if (TuNgay.value != '' && DenNgay.value == '') {
        //window.alert('Vui lòng nhập lại "Đến ngày"');
        //DenNgay.select();
        //r = false;
        DenNgay.value = '01/01/2200'
    }
    if (TuNgay.value != '' && DenNgay.value != '' && TruNgay(TuNgay.value, DenNgay.value) < 0) {
        //window.alert('"Đến ngày" phải lớn hơn hoặc bằng "Từ ngày"\nXin vui lòng nhập lại ngày tháng');
        //DenNgay.select();
        //r = false;
        var tempTuNgay = TuNgay.value;
        var tempDenNgay = DenNgay.value;
        DenNgay.value = tempTuNgay;
        TuNgay.value = tempDenNgay;
    }
    //return r;
}

function KiemTra_TuNgay_DenNgay(TuNgay_ID, DenNgay_ID) {
    var r = true;
    var TuNgay = document.getElementById(TuNgay_ID);
    var DenNgay = document.getElementById(DenNgay_ID);
    if (TuNgay.value == '' && DenNgay.value != '') {        
        window.alert('Vui lòng nhập lại "Từ ngày"');
        TuNgay.select();
        r = false;
    }
    if (TuNgay.value != '' && DenNgay.value == '') {
        window.alert('Vui lòng nhập lại "Đến ngày"');
        DenNgay.select();
        r = false;        
    }
    if (TuNgay.value != '' && DenNgay.value != '' && TruNgay(TuNgay.value, DenNgay.value) < 0) {
        window.alert('"Đến ngày" phải lớn hơn hoặc bằng "Từ ngày"\nXin vui lòng nhập lại ngày tháng');
        DenNgay.select();
        r = false;
    }
    return r;
}