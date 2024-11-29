<template>
  <v-container>
    <!-- Toolbar -->
    <v-toolbar flat>
      <v-toolbar-title>Lista de Produtos</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn color="primary" @click="goToCreateProduto"
        >Criar Novo Produto</v-btn
      >
    </v-toolbar>

    <!-- Data Table -->
    <v-data-table
      :headers="headers"
      :items="tabelaData"
      item-value="id"
      :loading="loading"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-text-field
          v-model="search"
          label="Pesquisar"
          class="mx-4"
          clearable
        ></v-text-field>
      </template>

      <template v-slot:body="{ items }">
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>{{ item.nome }}</td>
            <td>{{ item.descricao }}</td>
            <td>
              <v-btn icon color="primary" @click="editProduto(item)">
                <v-icon>mdi-pencil</v-icon>
              </v-btn>
              <v-btn icon color="red" @click="deleteProduto(item)">
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </td>
          </tr>
        </tbody>
      </template>

      <template v-slot:no-data>
        <v-alert type="info" :value="true">Nenhum Produto Encontrado.</v-alert>
      </template>

      <template v-slot:progress>
        <v-progress-linear color="primary" indeterminate></v-progress-linear>
      </template>
    </v-data-table>
  </v-container>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      tabelaData: [],
      headers: [
        { text: "Nome", value: "nome" },
        { text: "Descrição", value: "descricao" },
        { text: "Ações", value: "acoes", sortable: false },
      ],
      search: "",
      loading: false,
    };
  },
  mounted() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      this.loading = true;
      try {
        const responseProdutos = await axios.get(
          "https://localhost:7237/api/Produtos"
        );
        this.tabelaData = responseProdutos.data;
      } catch (error) {
        console.error("Erro ao carregar os dados:", error);
      } finally {
        this.loading = false;
      }
    },
    goToCreateProduto() {
      this.$router.push({ name: "createProduto" });
    },
    editProduto(item) {
      this.$router.push({
        name: "updateProduto",
        params: { id: item.produtoId },
      });
    },
    async deleteProduto(item) {
      if (
        confirm(`Tem certeza que deseja deletar o Produto \"${item.nome}\"?`)
      ) {
        try {
          await axios.delete(
            `https://localhost:7237/api/Produtos/${item.produtoId}`
          );
          this.fetchData();
          alert("Produto deletado com sucesso!");
        } catch (error) {
          console.error("Erro ao deletar o produto:", error);
          alert("Erro ao tentar deletar o produto.");
        }
      }
    },
  },
};
</script>

<style scoped>
.mx-4 {
  margin-left: 16px;
  margin-right: 16px;
}
</style>
