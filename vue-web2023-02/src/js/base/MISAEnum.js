const MISAEnum = {
    Gender:{
        Female:0,
        Male:1,
        Other:2
    },
    FormMode:{
        Add:0,
        Update:1,
        Duplicate:2
    },
    ToastMode:{
        Success:0,
        Error:1
    },
    DialogMode:{
        dataInvalid:0,
        dataChange:1,
        removeEmployee:2,
        removeEmployees : 3,
        settingsChange:4,
        settingsDefault:5
    },
    SortMode:{
        desc:1,
        asc:2
    },
    FilterMode:{
        empty:1,
        notEmpty:2,
        equal:3,
        different:4,
        contain:5,
        notContain:6,
        startWith:7,
        endWith:8,
        less:9,
        lessOrEqual:10,
        bigger:11,
        biggerOrEqual:12
    }
}

export default MISAEnum;