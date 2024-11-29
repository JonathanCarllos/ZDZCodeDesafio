<template>
  <v-container>
      <v-toolbar flat>
          <v-toolbar-title>Lista de Categorias</v-toolbar-title>

          <v-spacer></v-spacer>
          <v-btn color="primary" @click="goToCreateCategoria">Criar Nova Categoria</v-btn>
      </v-toolbar>

      <v-data-table
          :headers="headers"
          :items="tabelaData"
          item-key="categoriaId"
          :loading="loading"
      >
          <template v-slot:item="{ item }">
              <tr>                  
                  <td>{{ item.nome }}</td>
                  <td>{{ item.descricao }}</td>
                  <td>
                      <v-btn icon color="primary" @click="editCategoria(item)">
                          <v-icon>mdi-pencil</v-icon>
                      </v-btn>
                      <v-btn icon color="red" @click="deleteCategoria(item)">
                          <v-icon>mdi-delete</v-icon>
                      </v-btn>
                  </td>
              </tr>
          </template>

          <template v-slot:no-data>
              <v-alert type="info" :value="true">
                  Nenhuma categoria encontrada.
              </v-alert>
          </template>

          <template v-slot:error>
              <v-alert type="error" :value="true">
                  Ocorreu um erro ao carregar os dados.
              </v-alert>
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
              const response = await axios.get("https://localhost:7237/api/Categorias");
              this.tabelaData = response.data;
          } catch (error) {
              console.error("Erro ao carregar os dados:", error);
          } finally {
              this.loading = false;
          }
      },
      goToCreateCategoria() {
          this.$router.push({ name: "createCategoria" });
      },
      editCategoria(item) {
          this.$router.push({ name: "updateCategoria", params: { id: item.categoriaId } });
      },
      async deleteCategoria(item) {
          if (confirm(`Tem certeza que deseja deletar a categoria ${item.nome}?`)) {
              try {
                  await axios.delete(`https://localhost:7237/api/Categorias/${item.categoriaId}`);
                  this.fetchData();
                  alert("Categoria deletada com sucesso!");
              } catch (error) {
                  console.error("Erro ao deletar a categoria:", error);
                  alert("Não foi possível deletar a categoria.");
              }
          }
      },
  },
};
</script>
