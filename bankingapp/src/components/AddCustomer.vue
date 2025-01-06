<template>
    <div class="mt-5">
        <h1>Add Customer</h1>

    </div>
    <div class="container mt-4">
        <form>
            <div class="d-flex gap-3">
                <div class="mb-3">
                    <label for="firstName" class="form-label">First Name</label>
                    <input type="text" class="form-control" id="firstName" v-model="customerToBeAdded.firstName">
                </div>
                <div class="mb-3">
                    <label for="lastName" class="form-label">Last Name</label>
                    <input type="text" class="form-control" id="lastName" v-model="customerToBeAdded.lastName">
                </div>
                <div class="mb-3">
                    <label for="phone" class="form-label">Phone</label>
                    <input type="text" class="form-control" id="phone" v-model="customerToBeAdded.phone">
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="email" class="form-control" id="email" v-model="customerToBeAdded.email">
                </div>
                <div class="mb-3">
                    <label for="address" class="form-label">Address</label>
                    <input type="text" class="form-control" id="address" v-model="customerToBeAdded.address">
                </div>
                <div class="mb-3">
                    <label for="city" class="form-label">City</label>
                    <input type="text" class="form-control" id="city" v-model="customerToBeAdded.city">
                </div>
                <div class="mb-3">
                    <label for="dob" class="form-label">DOB</label>
                    <input type="date" class="form-control" id="dob" v-model="customerToBeAdded.dob">
                </div>
            </div>
            <button type="button" class="btn btn-primary" @click="addCustomer">Add Customer</button>
        </form>
    </div>
</template>

<script setup>
import { createCustomerApiForGoLang } from '@/script/EmployeeService';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()

const customerToBeAdded = ref({
    firstName: '',
    lastName: '',
    phone: '',
    address: '',
    email: '',
    city: '',
    dob: '',
});

const addCustomer = async () => {
    try {
        const response = await createCustomerApiForGoLang(customerToBeAdded.value.firstName, customerToBeAdded.value.lastName, customerToBeAdded.value.phone, customerToBeAdded.value.email, customerToBeAdded.value.address, customerToBeAdded.value.city, customerToBeAdded.value.dob);
        if (response.status === 201) {
            alert('Customer Added');
            router.push('/')
        }
        else {
            alert('Customer Not Added');
        }
    } catch (error) {
        alert(error.response.data.message)
        console.log(error);
    }
}
</script>
<style scoped></style>