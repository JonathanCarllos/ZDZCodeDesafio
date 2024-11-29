<template>
    <v-container>  
      <v-toolbar flat>
        <v-toolbar-title>Criar Nova Categoria</v-toolbar-title>
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
          v-model="dataCadastro"
          label="Data de Cadastro"
          type="date"
          :rules="[rules.required]"
          required
        ></v-text-field>
  
        <v-btn
          :disabled="!valid"
          color="success"
          @click="submitForm"
        >
          Criar Categoria
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
        dataCadastro: "", 
        rules: {
          required: value => !!value || "Este campo é obrigatório",
        },
      };
    },
    methods: {
    
      async submitForm() {
        try {          
          const categoria = {
            nome: this.nome,
            descricao: this.descricao,
            dataCadastro: this.dataCadastro,
          };
            
          const response = await axios.post("https://localhost:7237/api/Categorias", categoria);
          console.log("Categoria criada com sucesso:", response.data);
            
          this.$router.push({ name: 'categoria' }); 
        } catch (error) {
          console.error("Erro ao criar categoria:", error);
        }
      },
        
      goBack() {
        this.$router.push({ name: 'categoria' });
      }
    },
  };
  </script>
  
  <style scoped>  
  .v-container {
    max-width: 600px;
    margin-top: 20px;
  }
  </style>
  