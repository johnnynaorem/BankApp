<template>
    <div class="modal fade" id="customerModal" tabindex="-1" aria-labelledby="customerModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="customerModalLabel">{{ title }}</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                        ref="closeModalButton"></button>
                </div>

                <!-- Modal Body -->
                <div class="modal-body">
                    <form @submit="handleSubmit">
                        <div class="mb-3">
                            <label for="firstName" class="form-label">First Name</label>
                            <input type="text" class="form-control" id="firstName" v-model="localCustomer.firstName"
                                minlength="5" required />
                        </div>
                        <div class="mb-3">
                            <label for="lastName" class="form-label">Last Name</label>
                            <input type="text" class="form-control" id="lastName" v-model="localCustomer.lastName" />
                        </div>
                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone</label>
                            <input type="text" class="form-control" id="phone" v-model="localCustomer.phone" required />
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" class="form-control" id="email" v-model="localCustomer.email"
                                required />
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Address</label>
                            <input type="text" class="form-control" id="address" v-model="localCustomer.address" />
                        </div>
                        <div class="mb-3">
                            <label for="city" class="form-label">City</label>
                            <input type="text" class="form-control" id="city" v-model="localCustomer.city" />
                        </div>
                        <div class="mb-3" v-if="localCustomer.dob !== undefined">
                            <label for="dob" class="form-label">DOB</label>
                            <input type="date" class="form-control" id="dob" v-model="localCustomer.dob" />
                        </div>

                        <button type="submit" class="btn btn-primary">
                            {{ title === 'Add Customer' ? 'Add' : 'Update' }} Customer
                        </button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- Edit Modal -->
    <!-- * Not Use -->
    <!-- <div class="modal fade" id="editCustomerModal" tabindex="-1" aria-labelledby="editCustomerModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="editCustomerModalLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                        ref="editCloseModal"></button>
                </div>
                <div class="modal-body">
                    <form @submit="handleSubmit">
                        <div class="mb-3">
                            <label for="firstName" class="form-label">First Name</label>
                            <input type="text" class="form-control" id="firstName" v-model="localCustomer.firstName"
                                minlength="5" required>
                        </div>
                        <div class="mb-3">
                            <label for="lastName" class="form-label">Last Name</label>
                            <input type="text" class="form-control" id="lastName" v-model="localCustomer.lastName">
                        </div>
                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone</label>
                            <input type="text" class="form-control" id="phone" v-model="localCustomer.phone">
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" class="form-control" id="email" v-model="localCustomer.email">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Address</label>
                            <input type="text" class="form-control" id="address" v-model="localCustomer.address">
                        </div>
                        <div class="mb-3">
                            <label for="city" class="form-label">City</label>
                            <input type="text" class="form-control" id="city" v-model="localCustomer.city">
                        </div>
                        <button type="sumbit" class="btn btn-primary">Update Customer</button>
                    </form>
                </div>
            </div>
        </div>
    </div> -->
</template>

<script>
export default {
    name: 'ModalForAddCustomerAndEditCustomer',
    props: {
        customer: {
            type: Object,
            required: true
        },
        title: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            localCustomer: { ...this.customer }
        }
    },
    emits: ['return-Customer'],
    methods: {
        handleSubmit() {
            this.$emit('return-Customer', this.localCustomer);
        }
    },
    watch: {
        customer(newValue) {
            this.localCustomer = { ...newValue }
        }
    }
}

</script>