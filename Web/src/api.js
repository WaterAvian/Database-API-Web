const rootUrl = "http://localhost:5062/api/";

const READY_STATE_COMPLETE = 4,
    HTTP_OK = 200,
    HTTP_CREATED = 201,
    HTTP_NO_CONTENT = 204,
    HTTP_UNAUTHORIZED = 401,
    HTTP_FORBIDDEN = 403,
    HTTP_NOT_FOUND = 404,
    HTTP_CONFLICT = 409;

function API(callee, dataLoadingVm, dataLoadingFlag, errorHandler) {

    let spinner = null,
        moreComing = false,
        deg = 0;
    const
        openRequests = [],
        keepSpinning = function () {
            moreComing = true;
        },
        spinnerStart = function () {
            const
                overlay = document.getElementsByClassName('header-overlay');
            if (spinner === null && overlay && overlay.length) {
                spinner = setInterval(() => {
                    deg = (deg + 1) % 360;
                    overlay[0].style.transform = "rotate(" + deg + "deg)";
                }, 10);
            }
        },
        spinnerStop = function () {
            const finisher = () => {
                const
                    overlay = document.getElementsByClassName('header-overlay');
                if (overlay && overlay.length) {
                    deg = Math.max(deg - 5, 0);
                    overlay[0].style.transform = "rotate(" + deg + "deg)";
                    if (deg > 0)
                        setTimeout(finisher, 1);

                }
            };
            moreComing = false;
            if (spinner !== null) {
                clearInterval(spinner);
                spinner = null;
                if (deg > 0)
                    finisher();
            }
        },
        beginRequest = function (request) {
            if (!openRequests.length)
                spinnerStart(); // create and show the spinner
            openRequests.push(request);
        },
        endRequest = function (request) {
            let ndx = openRequests.findIndex(function (req) {
                return request === req;
            });
            if (ndx >= 0)
                openRequests.splice(ndx, 1);
            if (!openRequests.length && !moreComing)
                spinnerStop();
        },
        getHttpRequest = function (verb, url, resolve, reject, headers) {

            const
                appendHeaders = function (headers) {
                    for (var headerKey in headers) {
                        if (headers.hasOwnProperty(headerKey)) {
                            request.setRequestHeader(headerKey, headers[headerKey]);
                        }
                    }
                },
                toJson = function (data) {
                    return resolve(typeof data === "string" ?
                        JSON.parse(data)
                        : data);
                },
                request = new XMLHttpRequest();

            beginRequest(request);

            if (url.substr(0, 4) !== "http" && url.substr(0, 1) !== "/")
                url = rootUrl + url;

            request.open(verb, url, true);
            request.setRequestHeader("Content-Type", "application/json; charset=utf-8");

            appendHeaders({
                "Access-Control-Allow-Origin": "*",
                "cache-control": "no-cache",
                "accept": "*/*",
                "expires": "-1"
            });

            if (typeof headers === "object") {
                appendHeaders(headers);
            }

            request.onloadend = function () {
                // network errors do not trigger onerror, so do it here 
                if (request.status === 404)
                    request.onerror(`${url} replied 404`);
            };

            request.onreadystatechange = function () {
                if (request.readyState === READY_STATE_COMPLETE) {
                    // This is called even on 404 etc, so check the status
                    const successful = request.status && request.status >= HTTP_OK && request.status < 300,
                        noData = request.status === HTTP_NO_CONTENT
                            || (successful && request.response === ""),
                        header = request.getResponseHeader("content-type"),
                        jsonResponse = header && header.indexOf("application/json") >= 0,
                        data = noData ? undefined : jsonResponse ? toJson(request.response) : request.response;

                    endRequest(request);

                    if (successful) {
                        resolve(data);
                    }
                    else if (request.status === HTTP_UNAUTHORIZED || request.status === HTTP_FORBIDDEN) {
                        reject("You do not have permission to access the requested resource.  Please contact support.");
                    }
                    else if (request.status === HTTP_NOT_FOUND) {
                        reject(Error("Not Found"));
                    }
                    reject(data);
                }
            };

            // Handle network errors
            request.onerror = function (err) {

                console.log(`NETWORK ERROR ${err}`);

                reject(Error("Network Error"));
            };

            return request;
        }

    this.put = function put(url, data, headers) {
        return new Promise(function (resolve, reject) {
            getHttpRequest("PUT", url, resolve, reject, headers).send(JSON.stringify(data));
        }).then(function (output) {
            return typeof output === "undefined"
                ? data
                : output;
        });
    }

    this.remove = function remove(url, data, headers) {
        return new Promise(function (resolve, reject) {
            getHttpRequest("DELETE", url, resolve, reject, headers).send(JSON.stringify(data));
        });
    }

    this.post = function post(url, data, options, headers) {
        options = Object.assign({ }, options);
        return new Promise(function (resolve, reject) {
            const req = getHttpRequest("POST", url, resolve, reject, headers);
            if (data) {
                req.send(JSON.stringify(data));
            } else {
                req.send();
            }
        });
    }

    this.get = function get(url, headers) {
        return new Promise(function (resolve, reject) {
            try {
                getHttpRequest("GET", url, resolve, reject, headers).send();
            } catch (err) {
                debugger;
            }
        });
    }

    Object.defineProperty(this, 'rootUrl', {
        get: function () {
            return rootUrl;
        },
        set: function (_) {
            rootUrl = _;
        }
    });
};

export default API;
