<template>
  <div>
    <h1>Driver Grid</h1>
    <div class="filters">
      <input v-model="filters.name" placeholder="Filter by name" />
      <input v-model="filters.surname" placeholder="Filter by surname" />
      <input v-model="filters.nationality" placeholder="Filter by nationality" />
    </div>
    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Surname</th>
          <th>Nationality</th>
          <th>Birthday</th>
          <th>URL</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="driver in filteredDrivers" :key="driver.driverId">
          <td>{{ driver.name }}</td>
          <td>{{ driver.surname }}</td>
          <td>{{ driver.nationality }}</td>
          <td>{{ formatDate(driver.birthday) }}</td>
          <td><a :href="driver.url" target="_blank">Link</a></td>
        </tr>
      </tbody>
    </table>
    <div class="pagination">
      <button @click="prevPage" :disabled="page === 1">Previous</button>
      <span>Page {{ page }} of {{ totalPages }}</span>
      <button @click="nextPage" :disabled="page === totalPages">Next</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed } from 'vue';
import axios from 'axios';

interface Driver {
  name: string;
  surname: string;
  nationality: string;
  birthday: string;
  url: string;
  driverId: string;
}

export default defineComponent({
  name: 'GridComponent',
  setup() {
    const drivers = ref<Driver[]>([]);
    const filters = ref({ name: '', surname: '', nationality: '' });
    const page = ref(1);
    const pageSize = 5;

    const fetchDrivers = async () => {
      try {
        const response = await axios.get<Driver[]>('https://localhost:7038/api/Driver');
        drivers.value = response.data;
        console.log('Fetched drivers:', drivers.value);
      } catch (error) {
        console.error('Error fetching drivers:', error);
      }
    };

    const filteredDrivers = computed(() => {
      return drivers.value
        .filter(driver =>
          driver.name.toLowerCase().includes(filters.value.name.toLowerCase())
        )
        .filter(driver =>
          driver.surname.toLowerCase().includes(filters.value.surname.toLowerCase())
        )
        .filter(driver =>
          driver.nationality
            .toLowerCase()
            .includes(filters.value.nationality.toLowerCase())
        )
        .slice((page.value - 1) * pageSize, page.value * pageSize);
    });

    const totalPages = computed(() => Math.ceil(drivers.value.length / pageSize));

    const prevPage = () => {
      if (page.value > 1) page.value--;
    };

    const nextPage = () => {
      if (page.value < totalPages.value) page.value++;
    };

    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      return date.toLocaleDateString();
    };

    fetchDrivers();

    return {
      filters,
      filteredDrivers,
      page,
      totalPages,
      prevPage,
      nextPage,
      formatDate,
    };
  },
});
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

th {
  background-color: #f4f4f4;
}

.filters {
  margin-bottom: 20px;
}

.pagination {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}

button {
  margin: 0 5px;
  padding: 5px 10px;
  cursor: pointer;
}
</style>