import 'jquery'
import Handlebars from 'handlebars'

export default {
    load: (name) => {
        var url = `app/templates/${name}.handlebars`;

        return new Promise((resolve, reject) => {
            $.ajax({
                url: url,
                success: (resp) => resolve(Handlebars.compile(resp)),
                error: (err) => reject(err)
            })
        })
    }
}