<template>
    <div>
        <h1>{{ msgPlusCounter }}</h1>
        <div>
            <div>SiteUsers</div>
            <div>
                <li v-for="item in siteUsers">
                    {{ item.id }} - {{item.firstname}} {{item.lastname}}
                </li>
            </div>
        </div>
        <div style="user-select: none;">
            <button @click="incrementCounter">Plus One</button>
            <a @click="decrementCounter">Minus One</a>
            <span> Count: {{counter}}</span>
        </div>
        <div>
            <div>PostGroups</div>
            <li v-for="item in postGroups">
                {{ item.groupName }}
            </li>
        </div>
    </div>
    <button @click="test">Click here for test</button>


    <AddUser></AddUser>
</template>
<style>
    a {
        padding: 0 10px;
        color: blue;
        text-decoration: underline;
    }
    a:hover {
        color: green;
        text-decoration: none;
        cursor: pointer;
    }
</style>
<script>
    import API from '../api.js';
    export default {
        name: 'Test',
        props: {
        },
        data() {
            return {
                msg: 'hello world',
                siteUsers: null,
                postGroups: null,
                counter: 0

            }
        },
        methods: {
            test: function () {
                debugger;
                this.test2('Mick', 'Lake');
            },
            test2: function () {
                const arg1 = arguments[0],
                    arg2 = arguments[1];
                const ourArgs = Array.prototype.slice.call(arguments);
                debugger;
            },
            decrementCounter: function () {
                this.counter--;
            },
            incrementCounter: function () {
                this.counter++;
            },
            getSiteUsers: function () {
                const vm = this;
                this.api.get("SiteUser/all").then(
                    response => {
                        vm.siteUsers = response;
                    }
                );
            },
            getPostGroups: function () {
                const vm = this;
                this.api.get("PostGroup/all").then(
                    response => {
                        vm.postGroups = response;
                    }
                );
            },
            addSiteUser: function () {
                const vm = this;
                this.api.post("SiteUser/ups", { firstname: firstname, lastname: lastname }).then(response => {
                    vm.siteUsers = response;
                    // clear textboxes
                });
            }

        },
        computed: {
            msgPlusCounter: function () {
                return `${this.msg}${this.counter}`
            }
        },
        created: function () {
            this.api = new API("Home");
        },
        mounted: function () {
//            this.addSiteUser(new Date().toLocaleTimeString(), 'LastName');

            this.getSiteUsers();
            this.getPostGroups();
            this.msg = `The api is at: ${this.api.rootUrl}`;
        }
    }
</script>
