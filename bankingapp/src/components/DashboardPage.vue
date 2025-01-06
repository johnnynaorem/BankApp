<template>
    <main class="mt-5">
        <div class="d-flex justify-content-between">
            <h2>Customer List</h2>
            <button type="button" class="add-new-movie-btn" data-bs-toggle="modal" data-bs-target="#customerModal"
                @click="prepareModal('add')">Add New Customer</button>
        </div>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" @input="search" v-model="searchValue" type="search" placeholder="Search" />
            </div>
            <div class="table-mapper">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th scope="col">Sl. No.</th>
                            <th scope="col">Full Name

                            </th>
                            <th scope="col">Account Number

                            </th>
                            <th scope="col">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(customer, i) in filterCustomer" :key="i">
                            <th>{{ i + 1 }}</th>

                            <td>{{ customer.firstName + " " + customer.lastName }}</td>

                            <td>{{ customer.accountNumber }}</td>

                            <td>
                                <button type="button" class="btn btn-primary" data-bs-toggle="modal"
                                    data-bs-target="#customerModal" @click="prepareModal('edit', customer)">
                                    Edit
                                </button>
                                <button type="button" class="btn btn-danger reserve-btn"
                                    @click="DeleteCustomer(customer.customerId)">Delete
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div>
            <h3>Go Lang Part</h3>
            <div class="table-mapper">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th scope="col">Sl. No.</th>
                            <th scope="col">Full Name

                            </th>
                            <th scope="col">Account Number

                            </th>
                            <th scope="col">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(customer, i) in customerFromGoLangAPI" :key="i">
                            <th>{{ i + 1 }}</th>

                            <td>{{ customer.FirstName + " " + customer.LastName }}</td>

                            <td>{{ customer.AccountNumber }}</td>

                            <td>
                                <button type="button" class="btn btn-primary">
                                    Edit
                                </button>
                                <button type="button" class="btn btn-danger reserve-btn">Delete
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <!-- ? Call the Modal component here -->

        <Modal :customer="modalCustomer" :title="modalTitle" @return-Customer="handleCustomer" />

    </main>
</template>
<script setup>
import { createCustomer, deleteCustomer, getAllCustomers, getAllCustomersApiForGoLang, updateCustomerAPI } from '@/script/EmployeeService';
import { computed, onMounted, ref } from 'vue';
import Modal from './Modal/Modal.vue';


const customer = ref([]);
const customerFromGoLangAPI = ref([]);
const searchValue = ref('');
const modalTitle = ref('Add Customer');



const modalCustomer = ref({});


const prepareModal = (mode, customer = null) => {
    if (mode === 'add') {
        modalTitle.value = 'Add Customer';
        modalCustomer.value = {
            customerId: 0,
            firstName: '',
            lastName: '',
            phone: '',
            email: '',
            address: '',
            city: '',
            dob: '',
        };
    } else if (mode === 'edit' && customer) {
        modalTitle.value = 'Edit Customer';
        modalCustomer.value = { ...customer };
    }
};


const handleCustomer = async (customerDetails) => {
    try {
        if (modalTitle.value === 'Add Customer') {
            addCustomer(customerDetails);
        } else {
            updateCustomer(customerDetails);
        }
        fetchingCustomerFromApi();
    } catch (err) {
        console.error(err.message);
        alert('Failed to save customer');
    }
};
const updateCustomer = async (updatedDetails) => {
    event.preventDefault();
    try {
        const response = await updateCustomerAPI(updatedDetails.customerId, updatedDetails.firstName, updatedDetails.lastName, updatedDetails.phone, updatedDetails.email, updatedDetails.address, updatedDetails.city);
        if (response.status === 200) {
            alert('Customer Updated');
            const modalElement = document.getElementsByClassName('btn-close')[0];
            console.log(modalElement);
            if (modalElement) {
                modalElement.click();
            }
            fetchingCustomerFromApi();
        }
        else {
            alert('Customer Not Updated');
        }
    } catch (error) {
        console.log(error);
    }
}

const DeleteCustomer = async (id) => {
    const response = await deleteCustomer(id);
    if (response.status === 200) {
        alert('Customer Deleted');
        fetchingCustomerFromApi();
    }
    else {
        alert('Customer Not Deleted');
    }
}

const addCustomer = async (customerDetails) => {
    event.preventDefault();
    try {
        const response = await createCustomer(customerDetails.firstName, customerDetails.lastName, customerDetails.phone, customerDetails.email, customerDetails.address, customerDetails.city, customerDetails.dob);
        if (response.status === 200) {
            alert('Customer Added');
            const modalElement = document.getElementsByClassName('btn-close')[0];
            console.log(modalElement);
            if (modalElement) {
                modalElement.click();
            }
            fetchingCustomerFromApi();
        }
        else {
            alert('Customer Not Added');
        }
    } catch (error) {
        console.log(error);
    }
}
const filterCustomer = computed(() => {
    const query = searchValue.value.toLowerCase();
    const filtered = customer.value.filter((customer) => {
        return customer.firstName.toLowerCase().includes(query) || customer.lastName.toLowerCase().includes(query) || customer.phone.includes(query) || customer.accountNumber.includes(query);
    });

    return filtered;
});

const fetchingCustomerFromApi = async () => {
    try {
        const returnCustomer = await getAllCustomers();
        if (returnCustomer.status === 200) {
            customer.value = returnCustomer.data;
        }
        const returnCustomerFromGoLangApi = await getAllCustomersApiForGoLang();
        if (returnCustomerFromGoLangApi.status == 200) {
            customerFromGoLangAPI.value = returnCustomerFromGoLangApi.data.customer;
        }
    } catch (err) {
        console.log(err);
    }
}

onMounted(() => {
    fetchingCustomerFromApi();
});

</script>