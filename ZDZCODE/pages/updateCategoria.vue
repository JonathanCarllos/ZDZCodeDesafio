<template>
    <v-container>
        <v-toolbar flat>
            <v-toolbar-title>Atualizar Categoria</v-toolbar-title>
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

            <v-btn :disabled="!valid" color="success" @click="submitForm">
                Salvar Alterações
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
            categoriaId: null, 
            nome: "",
            descricao: "",
            dataCadastro: "",
            rules: {
                required: value => !!value || "Este campo é obrigatório",
            },
        };
    },
    mounted() {
        this.loadCategoria();
    },
    methods: {
        async loadCategoria() {
            try {
                const { id: categoriaId } = this.$route.params;
                this.categoriaId = categoriaId; 
                const response = await axios.get(`https://localhost:7237/api/Categorias/${categoriaId}`);
                const categoria = response.data;

                this.nome = categoria.nome || "";
                this.descricao = categoria.descricao || "";
                this.dataCadastro = categoria.dataCadastro ? categoria.dataCadastro.split("T")[0] : "";
            } catch (error) {
                alert("Não foi possível carregar os dados da categoria.");
            }
        },
        async submitForm() {
            try {
                const categoria = {
                    categoriaId: this.categoriaId,
                    nome: this.nome,
                    descricao: this.descricao,
                    dataCadastro: this.dataCadastro,
                };
                const response = await axios.put(`https://localhost:7237/api/Categorias/${this.categoriaId}`, categoria);

                if (response.status === 200 || response.status === 204) {
                    alert("Categoria atualizada com sucesso!");
                    this.$router.push({ name: "categoria" });
                } else {
                    alert("Algo deu errado ao tentar atualizar a categoria.");
                }
            } catch (error) {
                alert("Não foi possível salvar as alterações. Verifique os dados e tente novamente.");
            }
        },
        goBack() {
            this.$router.push({ name: "categoria" });
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
