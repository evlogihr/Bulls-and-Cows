import 'jquery'
import Handlebars from 'handlebars'

export default {
    load: (name) => {
        var url = `app/templates/${name}.handlebars`,
            templateKey = `template-${name}`;

        let cachedTemplate = localStorage.getItem(templateKey);
        if (cachedTemplate) {
            return Promise.resolve(Handlebars.compile(cachedTemplate));
        } else {
            return new Promise((resolve, reject) => {
                $.ajax({
                    url: url,
                    success: (resp) => {
                        // localStorage.setItem(templateKey, resp);
                        resolve(Handlebars.compile(resp));
                    },
                    error: (err) => reject(err)
                })
            })
        }
    }
}