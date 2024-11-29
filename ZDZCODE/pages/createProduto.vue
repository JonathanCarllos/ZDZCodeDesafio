<template>
  <v-container>
    <v-toolbar flat>
      <v-toolbar-title>Criar Novo Produto</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn color="primary" @click="goBack">Voltar</v-btn>
    </v-toolbar>

    <v-form v-model="valid" @submit.prevent="submitForm">
      <v-text-field
        v-model="nome"
        label="Nome"
        :rules="[rules.required]"
        required
      ></v-text-field>

      <v-textarea
        v-model="descricao"
        label="Descrição"
        :rules="[rules.required]"
        required
      ></v-textarea>

      <v-text-field
        v-model="imagemUrl"
        label="Imagem URL"
        :rules="[rules.required]"
        required
      ></v-text-field>

      <v-checkbox v-model="estoque" label="Tem Estoque?"></v-checkbox>

      <v-text-field
        v-model="quantidade"
        label="Quantidade"
        type="number"
        :rules="[rules.required, rules.number]"
        required
      ></v-text-field>

      <v-text-field
        v-model="preco"
        label="Preço R$"
        type="number"
        :rules="[rules.required, rules.number]"
        required
      ></v-text-field>

      <v-text-field
        v-model="dataCadastro"
        label="Data de Cadastro"
        type="date"
        :rules="[rules.required]"
        required
      ></v-text-field>

      <v-select
        v-model="categoriaSelecionada"
        :items="categorias"
        item-value="id"
        item-text="nome"
        label="Categoria"
        :rules="[rules.required]"
        return-object
        required
      ></v-select>

      <v-btn :disabled="!valid" color="success" @click="submitForm">
        Criar Produto
      </v-btn>
    </v-form>
  </v-container>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      valid: false,
      nome: "",
      descricao: "",
      imagemUrl: "",
      estoque: false,
      quantidade: 0,
      preco: 0,
      dataCadastro: "",
      categorias: [],
      categoriaSelecionada: null, 
      rules: {
        required: (value) => !!value || "Este campo é obrigatório",
        number: (value) => !isNaN(value) || "Este campo deve ser um número",
      },
    };
  },
  created() {
    this.fetchCategorias(); 
  },
  watch: {
    categoriaSelecionada(newValue) {
      console.log("Categoria selecionada:", newValue); 
    }
  },
  methods: {
    async fetchCategorias() {
      try {
        const response = await axios.get("https://localhost:7237/api/Categorias");
        console.log("Categorias carregadas:", response.data);
        this.categorias = response.data;
      } catch (error) {
        console.error("Erro ao carregar categorias:", error);
      }
    },

    async submitForm() {
      if (!this.categoriaSelecionada || typeof this.categoriaSelecionada !== 'object' || !this.categoriaSelecionada.categoriaId
      ) {
        console.error("Categoria inválida ou não selecionada.");
        return;
      }

      try {
        const produto = {
          nome: this.nome,
          descricao: this.descricao,
          imagemUrl: this.imagemUrl,
          estoque: this.estoque,
          quantidade: this.quantidade,
          preco: this.preco,
          dataCadastro: this.dataCadastro,
          categoriaId: this.categoriaSelecionada.categoriaId
          , 
        };

        console.log("Produto a ser enviado:", produto);
        const response = await axios.post(
          "https://localhost:7237/api/Produtos",
          produto
        );
        console.log("Produto criado com sucesso:", response.data);

        this.$router.push({ name: "produtos" });
      } catch (error) {
        console.error("Erro ao criar produto:", error);
      }
    },

    goBack() {
      this.$router.push({ name: "produtos" });
    },
  },
};
</script>

<style scoped>
.v-container {
  max-width: 600px;
  margin-top: 20px;
}
</style>
