import 'jquery'

export default {
    get: (url, headers) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: url,
                type: 'GET',
                headers: headers,
                success: (data) => resolve(data),
                error: (err) => reject(err)
            })
        })
    },
    post: (url, data, headers) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                headers: headers,
                data: data,
                success: (data) => resolve(data),
                error: (err) => reject(err)
            })
        })
    },
    postJSON: (url, data, headers) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json',
                headers: headers,
                data: JSON.stringify(data),
                success: (data) => resolve(data),
                error: (err) => reject(err)
            })
        })
    },
    putJSON: (url, data, headers) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: url,
                type: 'PUT',
                contentType: 'application/json',
                headers: headers,
                data: JSON.stringify(data),
                success: (data) => resolve(data),
                error: (err) => reject(err)
            })
        })
    }
}