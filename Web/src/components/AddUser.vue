<template>
    <h2>Create an account:</h2>

    <div>
        <label>
            First name:
            <input v-model="firstname" />
        </label>
    </div>
    <div>
        <label>
            Last name:
            <input v-model="lastname" />
        </label>
    </div>
    <div>
        <label>
            Password:
            <input @keyup="pwChange" v-model="password" />
        </label>
    </div>
    <button @click="addUser">Create Account</button>

    <h2>List of created accounts:</h2>

    <li v-for="item in siteUsers">
        {{item.firstname}} {{item.lastname}}
    </li>
</template>

<script>
    import API from '../api.js';
    export default {
        name: 'Test',
        props: {

        },
        data() {
            return {
                siteUsers: null,
                firstname: null,
                lastname: null,
                password: null
            }
        },
        methods: {
            getSiteUsers: function () {
                const vm = this;
                this.api.get("SiteUser/all").then(
                    response => {
                        vm.siteUsers = response;
                    }
                );
            },
            pwChange: function () {
                if (typeof this.password == "string" && this.password.length) {
                    var i = this.password.indexOf('a');
                    if (i >= 0)
                        this.password = `${this.password.substr(0, i)}${this.password.substr(i + 1)}`;
                }
            },
            addUser: function () {
            //    const saveUser = function (response) {
            //        this.siteUsers = response;
            //        this.firstname = null;
            //        this.lastname = null;
            //        this.password = null;
            //    }.bind(this);

            //    this.api.post("SiteUser/ups", { firstname: this.firstname, lastname: this.lastname, password: this.password })
            //        .then(saveUser);

                const vm = this;
                this.api.post("SiteUser/ups", { firstname: vm.firstname, lastname: vm.lastname, password: vm.password })
                    .then(response => {
                        this.siteUsers = response;
                        this.firstname = null;
                        this.lastname = null;
                        this.password = null;
                    });
            }
        },
        created: function () {
            this.api = new API("Home");
        },
        mounted: function () {
            //            this.addSiteUser(new Date().toLocaleTimeString(), 'LastName');

            this.getSiteUsers();
            this.msg = `The api is at: ${this.api.rootUrl}`;
        }
    }
</script>

<style scoped>
</style>